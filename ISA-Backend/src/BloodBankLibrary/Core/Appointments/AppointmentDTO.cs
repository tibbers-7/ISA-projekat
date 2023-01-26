using BloodBankLibrary.Core.Materials.Enums;

namespace BloodBankLibrary.Core.Appointments
{
    public class AppointmentDTO
    {
        private int id;
        private int staffId;
        private int donorId;
        private string staffName;
        private string staffSurname;
        private string startDate;
        private int duration;
        private int centerId;
        private string centerName;  
        private string status;
        private byte[] qrCode;

        public AppointmentDTO() { }

        public AppointmentDTO(Appointment appt)
        {
            this.id=appt.Id;
            this.staffId=appt.StaffId;
            this.donorId=appt.DonorId;
            this.CenterId=appt.CenterId;
            this.duration=appt.Duration;

            this.status=appt.Status.ToString().ToLower();
            char[] letters = this.status.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            this.status= new string(letters);

            this.startDate = appt.StartDate.ToString("dd.MM.yyyy. HH:mm");
            this.qrCode = appt.QrCode;

        }

        public int StaffId { get => staffId; set => staffId = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get => donorId; set => donorId = value; }
        public int CenterId { get => centerId; set => centerId = value; }
        public string StaffName { get => staffName; set=> staffName = value; }
        public string StaffSurname { get => staffSurname; set => staffSurname = value; }
        public string CenterName { get => centerName; set => centerName = value; }
        public int Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
        public byte[] QrCode { get => qrCode; set => qrCode = value; }
    }
}
