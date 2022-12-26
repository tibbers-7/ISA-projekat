using BloodBankLibrary.Core.Materials.Enums;

namespace BloodBankLibrary.Core.Appointments
{
    public class AppointmentDTO
    {
        private int id;
        private int staffId;
        private int donorId;
        private string date;
        private int duration;
        private int centerId;
        private string status;

        public AppointmentDTO() { }

        public AppointmentDTO(Appointment appt)
        {
            this.id=appt.Id;
            this.staffId=appt.StaffId;
            this.donorId=appt.DonorId;
            this.duration=appt.Duration;

            this.status=appt.Status.ToString().ToLower();
            char[] letters = this.status.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            this.status= new string(letters);

            this.date = appt.StartDate.ToString("dd.MM.yyyy.");

        }

        public int StaffId { get => staffId; set => staffId = value; }
        public string Date { get => date; set => date = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get => donorId; set => donorId = value; }
        public int CenterId { get => centerId; set => centerId = value; }
        public int Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
    }
}
