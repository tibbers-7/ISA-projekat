using System;
using System.Collections.Generic;
using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Donors;
using BloodBankLibrary.Core.EmailSender;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Materials.QRGenerator;
using BloodBankLibrary.Core.Staffs;
using System.Linq;
using MimeKit.Encodings;

namespace BloodBankLibrary.Core.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBloodCenterRepository _bloodCenterRepository;


        private readonly IQRService _qRService;
        private readonly IBloodCenterService _centerService;
        private readonly IStaffService _staffService;
        private readonly IEmailSendService _emailSendService;
        private readonly IDonorService _donorService;
        public AppointmentService(IAppointmentRepository appointmentRepository, IBloodCenterRepository bloodCenterRepository,
                                    IQRService qRService,
                                    IBloodCenterService centerService,
                                    IStaffService staffService,
                                    IEmailSendService emailSendService,
                                    IDonorService donorService)
        {
            _appointmentRepository = appointmentRepository;
            _bloodCenterRepository = bloodCenterRepository;

            _qRService = qRService;
            _centerService = centerService;
            _staffService = staffService;
            _emailSendService = emailSendService;
            _donorService = donorService;

        }


        public void Create(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
        }


        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public void Update(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public IEnumerable<Appointment> GetEligible()
        {
            return _appointmentRepository.GetEligible();
           
        }

        public IEnumerable<Appointment> GetScheduled()
        {
           IEnumerable<Appointment> scheduled=_appointmentRepository.GetScheduled();
            List<Appointment> res=new List<Appointment>();
           foreach (Appointment appointment in scheduled)
            {
                    if (DateTime.Compare(appointment.StartDate.AddMinutes(appointment.Duration), DateTime.Now) >= 0)
                    {
                        res.Add(appointment);
                    }
           
            }
            return res;
        }


        // TODO: nmg da izvalim jel ovo dobavlja i one completed 
        // ostavicu zasad u servisu
        public IEnumerable<AppointmentDTO> GetScheduledByDonor(int donorId)
        {
            IEnumerable<Appointment> allScheduled = GetScheduled();
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach(Appointment appointment in allScheduled)
            {
                if (appointment.DonorId == donorId) res.Add(new AppointmentDTO(appointment));
            }
            return res;
            //return allScheduled.Where<Appointment>(a => a.DonorId == donorId);
        }

        public IEnumerable<Appointment> GetScheduledByCenter(int id)
        {
            return _appointmentRepository.GetScheduledByCenter(id);
        }

        public IEnumerable<Appointment> GetFutureByCenter(int id)
        {
            return _appointmentRepository.GetFutureByCenter(id);
        }

        public IEnumerable<Appointment> GetEligibleByCenter(int id)
        {
            return _appointmentRepository.GetEligibleByCenter(id);
        }

        //ovo popraviti da bude buducnost ako treba
        public IEnumerable<Appointment> GetByStaffId(int id)
        {
            return _appointmentRepository.GetByStaff(id);
        }

        public Appointment PrepareForSchedule(AppointmentDTO dto)
        {
            dto.Status = "SCHEDULED";
            var appointment = new Appointment(dto);
            //ako je false nije available
            if (!CheckIfCenterAvailable(appointment.CenterId, appointment.StartDate, appointment.Duration))
            {
                return null;
            }
            if (appointment.StaffId==0) appointment = AssignStaff(appointment);
            if (appointment.StaffId == 0) return null;
            appointment = GenerateAndSaveQR(appointment);
            return appointment;


        }

        public IEnumerable<BloodCenter> GetCentersForDateTime(string dateTime)
        {
            //gledamo samo scheduled, available mogu doci u obzir
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            IEnumerable<BloodCenter> bloodCenters = _bloodCenterRepository.GetAll();
            List<BloodCenter> availableCenters = new List<BloodCenter>();
           
            foreach(BloodCenter center in bloodCenters)
            { 
               if(CheckIfCenterAvailable(center.Id, parsedDateTime, 30))
                {
                    availableCenters.Add(center);
                }
            }
            return availableCenters;
        }

        public bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration)
        {
            List<Appointment> allCenterApps = GetScheduledByCenter(centerId).ToList();
            BloodCenter bloodCenter = _bloodCenterRepository.GetById(centerId);
            if (dateTime.Hour < bloodCenter.WorkTimeStart.Hour || dateTime.Hour > bloodCenter.WorkTimeEnd.Hour) return false;
            foreach (Appointment app in allCenterApps)
            {
                
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), dateTime, dateTime.AddMinutes(duration)))
                {
                    return false;
                }

             }
            return true;

        }

        public bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            //ako je pocetak prvog pre pocetka drugog, onda prvi mora i da bude zavrsen pre nego sto drugi pocne
            if (DateTime.Compare(start1, start2) < 0)
            {
                if (DateTime.Compare(end1, start2) > 0) return true;
                else return false;
            }
            //ako je pocetak drugog pre pocetka prvog, onda kraj drugog mora biti pre pocetka prvog
            else if(DateTime.Compare(start2,start1)<0)
            {
                if (DateTime.Compare(end2, start1) > 0) return true;
                else return false;
            }
            //ne mogu da pocnu u isto vreme
            return true;

        }

        //can the donor cancel the appt
        public bool CanDonorCancel(int apptId)
        {
            
            Appointment appt=GetById(apptId);
            DateTime cancelBy = appt.StartDate.AddDays(1);
            if (DateTime.Compare(cancelBy, DateTime.Now) < 0) return true;
            return false;

        }

        //prvo provera da li je prekasnoo da otkaze pregled pa ako nije napravi se kopija koja je available i kreira se u bazi
        //a ovaj se apdejtuje kao cancelled
        public bool CancelAppt(AppointmentDTO appointment)
        {
            if (!CanDonorCancel(appointment.Id)) return false;
            Appointment _appt= new Appointment(appointment);
            _appt.Status = AppointmentStatus.CANCELLED;
            _appointmentRepository.Update(_appt);
            //sad pravimo kopiju
            Appointment _newAppt = new Appointment { CenterId = _appt.CenterId, Duration = _appt.Duration, StaffId = _appt.StaffId, StartDate = _appt.StartDate, Status = AppointmentStatus.AVAILABLE };
            _appointmentRepository.Create(_newAppt);
            return true;
        }

        public object GetEligibleForDonor(int donorId, int centerId)
        {
            IEnumerable<Appointment> appointments = GetEligibleByCenter(centerId);
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Status == AppointmentStatus.CANCELLED && appointment.DonorId == donorId) continue;
                res.Add(new AppointmentDTO(appointment));
            }
            return res;
        }

        public Appointment GenerateAndSaveQR(Appointment appointment)
        {
            Staff staff = _staffService.GetById(appointment.StaffId);

            string filePath=appointment.DonorId.ToString()+"_"+appointment.CenterId.ToString()+"_"+appointment.StartDate.ToString("dd_MM_yyyy_HH_mm")+".jpg";

            byte[] qr = _qRService.GenerateQR(appointment.EmailInfo(
                                                         _centerService.GetById(appointment.CenterId).Name,
                                                         staff.Name +" "+ staff.Surname),filePath);
            appointment.QrCode = qr;
            Donor donor = _donorService.GetById(appointment.DonorId);
            string subject = "BloodCenter - Scheduled appointment information";
            string body = "Here is the QR code with your information:\n";

            filePath = "AppData\\" + filePath;

            donor.Email = "tibbers707@gmail.com";
            _emailSendService.SendWithQR(new Message(new string[] { donor.Email }, subject, body), qr,filePath);
            //_qRService.DeleteImage(filePath);

            return appointment;
            
        }

        public Appointment AssignStaff(Appointment apptToAssign)
        {
            List<Staff> staffs = _staffService.GetByCenterId(apptToAssign.CenterId).ToList();
            foreach(Staff staff in staffs)
            {
                List<Appointment> apptsByDateAndStaff = _appointmentRepository.GetByDateAndStaff(staff.Id, apptToAssign.StartDate).ToList();
                bool isAvailable = true;
                foreach(Appointment appointment in apptsByDateAndStaff)
                {
                    if (Overlaps(appointment.StartDate, appointment.StartDate.AddMinutes(appointment.Duration), apptToAssign.StartDate, apptToAssign.StartDate.AddMinutes(apptToAssign.Duration))) 
                        isAvailable = false;
                }
                if (!isAvailable) continue;
                apptToAssign.StaffId=staff.Id;
            }

            return apptToAssign;

        }

        private bool isStaffAvailable(int staffId, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public object GetAllByDonor(int id)
        {
            return _appointmentRepository.GetByDonor(id);
        }

        public IEnumerable<Donor> GetDonorsByCenterId(int centerId)
        {
            IEnumerable<int> donorIds = _appointmentRepository.GetDonorsByCenter(centerId);
            List<Donor> res = new List<Donor>();
            foreach (int donorId in donorIds)
            {
                res.Add(_donorService.GetById(donorId));
            }
            return res;
        }

    }
}
