namespace BloodBankLibrary.Core.Staffs
{
    public class Staff
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public int CenterId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public Staff() { }
    }
}
