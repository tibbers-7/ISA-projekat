namespace BloodBankAPI.Model
{
    public class Staff : Account
    {
        public int CenterId { get; set; }
        public BloodCenter BloodCenter { get; set; }
        public Staff() { }
    }
}
