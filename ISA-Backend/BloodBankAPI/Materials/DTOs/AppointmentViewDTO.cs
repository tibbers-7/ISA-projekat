namespace BloodBankAPI.Materials.DTOs
{
    public class AppointmentViewDTO
    {
        public int StaffId { get; set; }
        public string StartDate { get; set; }
        public int Duration { get; set; }
        public int DonorId { get; set; }
        public int CenterId { get; set; }
        public string StaffName { get; set; }
        public string StaffSurname { get; set; }
        public string CenterName { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public byte[] QrCode { get; set; }

        public AppointmentViewDTO() { }
    }
}
