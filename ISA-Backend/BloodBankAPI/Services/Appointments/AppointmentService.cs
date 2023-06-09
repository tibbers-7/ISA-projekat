using Microsoft.IdentityModel.Tokens;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using BloodBankAPI.Materials.DTOs;
using MimeKit.Cryptography;

namespace BloodBankAPI.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
         _unitOfWork = unitOfWork;
        }
        

        public async Task Create(Appointment appointment)
        {
           await _unitOfWork.AppointmentRepository.InsertAsync(appointment);
        }


        public async Task<IEnumerable<Appointment>> GetAll()
        {
           var apps =  await _unitOfWork.AppointmentRepository.GetAllAsync();

            return apps;
        }

        public void Update(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.Update(appointment);
        }

        public async Task<Appointment> GetById(int id)
        {
            return await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }

        /*
        public async Task<IEnumerable<Appointment>> GetScheduled()
        {
           IEnumerable<Appointment> scheduled = await _unitOfWork.AppointmentRepository
                .GetByConditionAsync(
                a => a.Status == Materials.Enums.AppointmentStatus.SCHEDULED && 
                DateTime.Compare(a.StartDate.AddMinutes(a.Duration),DateTime.Now) >= 0
                );
            /*
             * PROVERITIIIIIIIIIIIIIIIIII
            List<Appointment> res = new List<Appointment>();
            foreach (Appointment appointment in scheduled)
            {
                if (DateTime.Compare(appointment.StartDate.AddMinutes(appointment.Duration), DateTime.Now) >= 0)
                {
                    res.Add(appointment);
                }

            }
            return res;
            
            return scheduled;
        }

        public async Task<IEnumerable<Donor>> GetDonorsByCenterId(int centerId)
        {
            //ovde bi bio dobar cist query
            IEnumerable<Appointment> apps = await _unitOfWork.AppointmentRepository.GetByConditionAsync(app => app.CenterId == centerId);
            List<int> donorIds = new List<int>();
            foreach (Appointment app in apps)
            {
                donorIds.Add(app.DonorId);
            }
            //ovo je katastrofa mozda da imam 1 to 1 u bazi za appointmente i 1 to 1 za centre pa onda lazy loading iskljucim
            List<Donor> res = new List<Donor>();
            foreach (int donorId in donorIds)
            {
                res.Add(_donorService.GetById(donorId));
            }
            return res;
        }

        // TODO: nmg da izvalim jel ovo dobavlja i one completed 
        // ostavicu zasad u servisu
        public IEnumerable<AppointmentDTO> GetScheduledByDonor(int donorId)
        {
            List<Appointment> allScheduled = GetScheduled().ToList();
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach (Appointment appointment in allScheduled)
            {
                if (appointment.DonorId == donorId)
                {
                    AppointmentDTO dto = MakeDTO(appointment);
                    res.Add(dto);
                }
            }
            return res;
            //return allScheduled.Where<Appointment>(a => a.DonorId == donorId);
        }

        public IEnumerable<Appointment> GetScheduledByCenter(int id)
        {
            return _appointmentRepository.GetScheduledByCenter(id);
        }

        //TREBA
        public IEnumerable<AppointmentDTO> GetFutureByCenter(int id)
        {
            List<Appointment> future = _appointmentRepository.GetFutureByCenter(id).ToList();
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach (Appointment appointment in future)
            {
                res.Add(MakeDTO(appointment));
            }
            return res;
        }

        public IEnumerable<Appointment> GetEligibleByCenter(int id)
        {
            return _appointmentRepository.GetEligibleByCenter(id);
        }


        public Appointment PrepareForSchedule(AppointmentDTO dto)
        {
            var appointment = new Appointment(dto);
            List<Appointment> allCenterAppts = _appointmentRepository.GetEligibleByCenter(dto.CenterId).ToList();
            foreach (Appointment app in allCenterAppts)
            {
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), appointment.StartDate, appointment.StartDate.AddMinutes(appointment.Duration)))
                {

                    appointment = app;
                    appointment.Status = AppointmentStatus.SCHEDULED;
                    appointment.DonorId = dto.DonorId;
                    appointment = GenerateAndSaveQR(appointment, null);
                    return appointment;
                }
            }

            //ako je false nije available
            if (!CheckIfCenterAvailable(appointment.CenterId, appointment.StartDate, appointment.Duration))
            {
                SendQRCancelled(appointment, 1);
                return null;
            }
            if (appointment.StaffId == 0) appointment = AssignStaff(appointment);
            if (appointment.StaffId == 0) return null;
            appointment = GenerateAndSaveQR(appointment, null);
            return appointment;


        }


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
        //TREBA
        public IEnumerable<BloodCenter> GetCentersForDateTime(string dateTime)
        {
            //gledamo samo scheduled, available mogu doci u obzir
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            List<BloodCenter> bloodCenters = _bloodCenterRepository.GetAll().ToList();
            List<BloodCenter> availableCenters = new List<BloodCenter>();

            foreach (BloodCenter center in bloodCenters)
            {
                if (CheckIfCenterAvailable(center.Id, parsedDateTime, 30))
                {
                    availableCenters.Add(center);
                }
            }
            return availableCenters;
        }

        //TREBA
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
            else if (DateTime.Compare(start2, start1) < 0)
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

            Appointment appt = GetById(apptId);
            DateTime cancelBy = appt.StartDate.AddDays(1);
            if (DateTime.Compare(cancelBy, DateTime.Now) < 0) return false;
            return true;

        }

        //prvo provera da li je prekasno da otkaze pregled pa ako nije napravi se kopija koja je available i kreira se u bazi
        //a ovaj se apdejtuje kao cancelled
        public bool CancelAppt(AppointmentDTO appointment)
        {
            if (!CanDonorCancel(appointment.Id)) return false;
            Appointment _appt = GetById(appointment.Id);
            _appt.Status = AppointmentStatus.CANCELLED;
            _appointmentRepository.Update(_appt);
            SendQRCancelled(_appt, 3);
            //sad pravimo kopiju
            Appointment _newAppt = new Appointment { CenterId = _appt.CenterId, Duration = _appt.Duration, StaffId = _appt.StaffId, StartDate = _appt.StartDate, Status = AppointmentStatus.AVAILABLE };
            _appointmentRepository.Create(_newAppt);
            return true;
        }

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



        public Appointment GenerateAndSaveQR(Appointment appointment, string cancelReason)
        {
            Staff staff = _staffService.GetById(appointment.StaffId);
            var seed = 3;
            var random = new Random(seed);
            int rNum = random.Next();

            string filePath = appointment.DonorId.ToString() + "_" + appointment.CenterId.ToString() + "_" + appointment.StartDate.ToString("dd_MM_yyyy_HH_mm") + rNum.ToString() + ".jpg";

            byte[] qr = _qRService.GenerateQR(appointment.EmailInfo(
                                                         _centerService.GetById(appointment.CenterId).Name,
                                                         staff.Name + " " + staff.Surname, cancelReason), filePath);
            appointment.QrCode = qr;
            Donor donor = _donorService.GetById(appointment.DonorId);
            string subject = "BloodCenter - Appointment information";
            string body = "Here is the QR code with your information:\n";

            filePath = "AppData\\" + filePath;

            _emailSendService.SendWithQR(new Message(new string[] { "danabrasanac@gmail.com", "tibbers707@gmail.com" }, subject, body), qr, filePath);
            //_qRService.DeleteImage(filePath);

            return appointment;

        }


        public Appointment AssignStaff(Appointment apptToAssign)
        {
            List<Staff> staffs = _staffService.GetByCenterId(apptToAssign.CenterId).ToList();
            foreach (Staff staff in staffs)
            {
                apptToAssign.StaffId = staff.Id;
                bool isAvailable = IsStaffAvailable(apptToAssign);
                if (!isAvailable) continue;
                apptToAssign.StaffId = staff.Id;
                return apptToAssign;
            }

            return apptToAssign;

        }

        public bool IsStaffAvailable(Appointment appointment)
        {
            List<Appointment> staffAppts = _appointmentRepository.GetFutureByStaff(appointment.StaffId).ToList();
            foreach (Appointment appt in staffAppts)
            {
                if (Overlaps(appointment.StartDate, appointment.StartDate.AddMinutes(appointment.Duration), appt.StartDate, appt.StartDate.AddMinutes(appt.Duration))) return false;

            }
            return true;
        }

        public object GetAllByDonor(int id)
        {
            return _appointmentRepository.GetByDonor(id);
        }

        public IEnumerable<AppointmentDTO> GetHistoryForDonor(int donorId)
        {
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            List<Appointment> apps = _appointmentRepository.GetHistoryForDonor(donorId).ToList();
            foreach (Appointment appt in apps)
            {
                AppointmentDTO dto = MakeDTO(appt);
                res.Add(dto);
            }
            return res;
        }

        public AppointmentDTO MakeDTO(Appointment appointment)
        {
            AppointmentDTO dto = new AppointmentDTO(appointment);
            Staff staff = _staffService.GetById(appointment.StaffId);
            BloodCenter center = _centerService.GetById(appointment.CenterId);
            dto.StaffName = staff.Name;
            dto.StaffSurname = staff.Surname;
            dto.CenterName = center.Name;
            return dto;
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

        public void CompleteAppt(AppointmentDTO appointment)
        {
            Appointment appt = GetById(appointment.Id);
            appt.Status = AppointmentStatus.COMPLETED;
            GenerateAndSaveQR(appt, null);
            Update(appt);


        }
        */
        
    }
}
