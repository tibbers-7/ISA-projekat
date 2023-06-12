using Microsoft.IdentityModel.Tokens;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using BloodBankAPI.Materials.DTOs;
using MimeKit.Cryptography;
using AutoMapper;
using BloodBankAPI.Materials.Enums;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.QRGenerator;

namespace BloodBankAPI.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSendService _emailSendService;
        private readonly IQRService _qrService;
        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper, IEmailSendService emailSendService, IQRService qRService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSendService = emailSendService;
            _qrService = qRService;
        }
        

        public async Task Create(Appointment appointment)
        {
           await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
        }

        //svi ikada appointmenti
        public async Task<IEnumerable<AppointmentViewDTO>> GetAll()
        {
           var apps =  await _unitOfWork.AppointmentRepository.GetAllWithIncludeAsync("Center", "Staff");
           return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        
        public void Update(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Update(appointment);
        }

        public async Task<AppointmentViewDTO> GetById(int id)
        {
            Appointment appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
            return _mapper.Map<AppointmentViewDTO>(appointment);
        }

        //svi koji su ikada zavrsili app u centru tj. dali krv
        public async Task<IEnumerable<Donor>> GetDonorsByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(app => app.CenterId == centerId &&
            app.Status == AppointmentStatus.COMPLETED, "Donor", null);
            List<Donor> donors = new List<Donor>();
            foreach (Appointment app in apps)
            {
                donors.Add(app.Donor);
            }   
            return donors;
        }

        //zakazani u buducnosti za donora, koristi se eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetScheduledByDonor(int donorId)
        {
            IEnumerable<Appointment> apps =  await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.DonorId == donorId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        //svi zakazani u centru, koristi se eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetScheduledByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        //svi slobodni buduci u centru, eager loading
        public async Task<IEnumerable<AppointmentViewDTO>> GetAvailableByCenter(int centerId)
        {
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
            a.Status == Materials.Enums.AppointmentStatus.AVAILABLE &&
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(apps);
        }

        public async Task<Appointment> GeneratePredefined(GeneratePredefinedAppointmentDTO dto)
        {
            Appointment appointment = _mapper.Map<Appointment>(dto);
            await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
            return appointment;
        }

        public async Task<bool> IsStaffAvailable(GeneratePredefinedAppointmentDTO dto)
        {
            IEnumerable<Appointment> appointments = await _unitOfWork.AppointmentRepository.GetByConditionAsync(a => a.StaffId == dto.StaffId &&
            a.Status == Materials.Enums.AppointmentStatus.SCHEDULED);
            DateTime dateOfAppt = DateTime.Parse(dto.StartDate);
            foreach (Appointment appt in appointments)
            {
                if (Overlaps(dateOfAppt, dateOfAppt.AddMinutes(dto.Duration), appt.StartDate, appt.StartDate.AddMinutes(appt.Duration))) return false;

            }

            return true;
        }

        public async Task<bool> IsCenterAvailable(int centerId, string dateTime, int duration)
        {
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            IEnumerable<Appointment> allCenterApps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == centerId &&
             a.Status == Materials.Enums.AppointmentStatus.SCHEDULED, "Center", null);
            if (allCenterApps.IsNullOrEmpty()) return true;
            BloodCenter center = allCenterApps.ElementAt(0).Center;
            if (parsedDateTime.Hour < center.WorkTimeStart.Hour || parsedDateTime.Hour > center.WorkTimeEnd.Hour) return false;
            foreach (Appointment app in allCenterApps)
            {
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), parsedDateTime, parsedDateTime.AddMinutes(duration)))
                {
                    return false;
                }
            }
            return true;
        }

        //scheduled,completed,cancelled
        public async Task<IEnumerable<AppointmentViewDTO>> GetHistoryForDonor(int donorId)
        {
            IEnumerable<Appointment> donorHistory = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.DonorId == donorId &&
            a.Status != AppointmentStatus.AVAILABLE, "Center", "Staff");
            return _mapper.Map<IEnumerable<AppointmentViewDTO>>(donorHistory);
        }

        public async Task<IEnumerable<CenterDTO>> GetCentersForDateTime(string dateTime)
        {
            //gledamo samo scheduled, available mogu doci u obzir
            IEnumerable<BloodCenter> bloodCenters = await _unitOfWork.CenterRepository.GetAllAsync();
            List<BloodCenter> availableCenters = new List<BloodCenter>();
            foreach (BloodCenter center in bloodCenters)
            {
                //ovde nam je predefinisano vec da traje 30 min kad donor sam zakazuje
                if (await IsCenterAvailable(center.Id, dateTime, 30))
                {
                    availableCenters.Add(center);
                }
            }
            return _mapper.Map<List<CenterDTO>>(availableCenters);
        }

        //ako vec postoji available appointment u centru koji se preklapa s nasim, zakazujemo njega
        public async Task<Appointment> ScheduleIfAvailableAppointmentExists(AppointmentRequestDTO dto)
        {
            //eligible su available, centerid==centerid, i date time je posle datetimenow
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionWithIncludeAsync(a => a.CenterId == dto.CenterId &&
            DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0 && a.Status == Materials.Enums.AppointmentStatus.AVAILABLE, "Center", "Staff");
            DateTime date = DateTime.Parse(dto.StartDate);
            foreach (Appointment app in apps)
            {
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), date, date.AddMinutes(dto.Duration)))
                {
                    UpdateAvailableToScheduled(app, dto.Duration, date, dto.DonorId);
                    return app;
                }
            }
            return null;
        }

        private void UpdateAvailableToScheduled(Appointment app,int duration, DateTime date, int donorId ) {
            app.Status = AppointmentStatus.SCHEDULED;
            app.DonorId = donorId;
            app.StartDate = date;
            app.Duration = duration;
           // app = GenerateAndSaveQR(app, null);
            _unitOfWork.AppointmentRepository.Update(app);

        }

         public async Task<Appointment> GenerateDonorMadeAppointment(AppointmentRequestDTO dto)
         {
            //posto se ne poklapa ni sa jednim vec postojecim available kreiramo novi
            //ako je false nije available, ne mozemo da zakazmemo
            Appointment appointment = _mapper.Map<Appointment>(dto);
            if (! await IsCenterAvailable(dto.CenterId, dto.StartDate, dto.Duration))
            {
                SendQRCancelled(appointment, 1);
                return null;
            }
            //ako nije izabran staff bira se random, ako i dalje nema onda ne mozemo da zakazemo
            appointment = await AssignStaff(appointment);
            if (appointment == null) return null;
          //  appointment = GenerateAndSaveQR(appointment, null);
            await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
            return appointment;
         }

        private async Task<Appointment> AssignStaff(Appointment apptToAssign)
        {
            IEnumerable<Staff> staffs = await _unitOfWork.StaffRepository.GetByConditionAsync(s => s.BloodCenterId == apptToAssign.CenterId);
            foreach (Staff staff in staffs)
            {

                apptToAssign.StaffId = staff.Id;
                if (!await IsStaffAvailable(_mapper.Map<GeneratePredefinedAppointmentDTO>(apptToAssign))) continue;
                return apptToAssign;
            }

            return null;

        }


        /*
        //ovo se kreira prikaz za predefinisane preglede donora
        public IEnumerable<AppointmentDTO> GetEligibleForDonor(int donorId, int centerId)
        {
            //buduci cancelled za tog donors u tom centru
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            List<Appointment> futureCancelledByDonor = _appointmentRepository.GetCancelledByDonorCenter(donorId, centerId).ToList();
            List<Appointment> appointments = GetEligibleByCenter(centerId).ToList();
            if (!futureCancelledByDonor.IsNullOrEmpty())
            {
                foreach (Appointment appointment in appointments)
                {
                    bool isCancelled = false;
                    foreach (Appointment cancelled in futureCancelledByDonor)
                    {
                        //ako su u isto vreme 
                        if (appointment.StartDate.Equals(cancelled.StartDate)) { isCancelled = true; break; }
                        else isCancelled = false;
                    }

                    if (!isCancelled) res.Add(MakeDTO(appointment));
                }
            }
            else
            {
                foreach (Appointment appointment in appointments)
                {
                    res.Add(MakeDTO(appointment));
                }
            }
            return res;
        }


        public object GetAllByDonor(int id)
        {
            return _appointmentRepository.GetByDonor(id);
        }


        public IEnumerable<AppointmentDTO> GetScheduledForStaff(int id)
        {
            List<AppointmentDTO> ret = new List<AppointmentDTO>();
            List<Appointment> scheduled = _appointmentRepository.GetScheduledForStaff(id).ToList();
            foreach (Appointment appt in scheduled)
            {
                ret.Add(MakeDTO(appt));
            }
            return ret;
        }
        */
        public async Task CompleteAppt(AppointmentViewDTO appointment)
        {
            Appointment appt = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointment.Id);
            appt.Status = AppointmentStatus.COMPLETED;
         //   SendQRCompleted(appt.Id, null);
            Update(appt);
        }

        //prvo provera da li je prekasno da otkaze pregled pa ako nije napravi se kopija koja je available i kreira se u bazi
        //a ovaj se apdejtuje kao cancelled
        public async Task<bool> CancelAppt(AppointmentViewDTO appointment)
        {
            Appointment appt = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointment.Id);
            if (!CanDonorCancel(appt.StartDate)) return false;
            appt.Status = AppointmentStatus.CANCELLED;
            _unitOfWork.AppointmentRepository.Update(appt);
            SendQRCancelled(appt, 3);
            //sad pravimo kopiju
            Appointment newAppt = new Appointment { CenterId = appt.CenterId, Duration = appt.Duration, StaffId = appt.StaffId, StartDate = appt.StartDate, Status = AppointmentStatus.AVAILABLE };
            await _unitOfWork.AppointmentRepository.InsertAsync(newAppt);
            return true;
        }

        //can the donor cancel the appt
        private bool CanDonorCancel(DateTime startDate)
        {
            DateTime cancelBy = startDate.AddDays(1);
            if (DateTime.Compare(cancelBy, DateTime.Now) < 0) return false;
            return true;

        }

       

        public Appointment GenerateAndSaveQR(Appointment appointment, string cancelReason)
        {
            Staff staff = appointment.Staff;
            var seed = 3;
            var random = new Random(seed);
            int rNum = random.Next();
            string filePath = appointment.DonorId.ToString() + "_" + appointment.CenterId.ToString() + "_" + appointment.StartDate.ToString("dd_MM_yyyy_HH_mm") + rNum.ToString() + ".jpg";

            byte[] qr = _qrService.GenerateQR(appointment.EmailInfo(appointment.Center.Name, appointment.Staff.Name + " " + appointment.Staff.Surname, cancelReason), filePath);
            appointment.QrCode = qr;
           // Donor donor = _donorService.GetById(appointment.DonorId);
            string subject = "BloodCenter - Appointment information";
            string body = "Here is the QR code with your information:\n";

            filePath = "AppData\\" + filePath;

            _emailSendService.SendWithQR(new Message(new string[] { "danabrasanac@gmail.com", "tibbers707@gmail.com" }, subject, body), qr, filePath);
            //_qRService.DeleteImage(filePath);

            return appointment;

        }
        /*
        public void SendQRScheduled(Appointment appointment)
        {
            string data =
        appointment.EmailInfo(appointment.Center.Name, appointment.Staff.Name + " " + appointment.Staff.Surname);


        }

        public void SendQRCompleted(Appointment appointment)
        {
            string data = appointment.EmailInfo(appointment.Center.Name, appointment.Staff.Name + " " + appointment.Staff.Surname);


        }

        */
        public void SendQRCancelled(Appointment appointment, int code)
        {
            switch (code)
            {
                case 1:
                    GenerateAndSaveQR(appointment, " the appointment has already been scheduled by someone else.");
                    break;
                case 2:
                    GenerateAndSaveQR(appointment, " the staff was busy.");
                    break;
                case 3:
                    GenerateAndSaveQR(appointment, " you cancelled it. A strike was added to your account.");
                    break;
            }
        }


        private bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            //ako je pocetak prvog pre pocetka drugog, onda prvi mora i da bude zavrsen pre nego sto drugi pocne
            if (DateTime.Compare(start1, start2) < 0)
            {
                if (DateTime.Compare(end1, start2) > 0) return true;
                else return false;
            }
            //ako je pocetak drugog pre pocetka prvog, onda kraj drugog mora biti pre pocetka prvog
            else if (DateTime.Compare(start2, start1) < 0)
            {
                if (DateTime.Compare(end2, start1) > 0) return true;
                else return false;
            }
            //ne mogu da pocnu u isto vreme
            return true;

        }


    }
}
