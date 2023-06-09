namespace BloodBankAPI.Materials.DTOs
{
    public class AppointmentViewDTO
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string Duration { get; set; }
        public string StaffFullName { get; set; }
        public string CenterName { get; set; }
        public string Status { get; set; }

        public AppointmentViewDTO() { }
    }
}
