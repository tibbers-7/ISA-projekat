namespace BloodBankLibrary.Core.Addresses
{
    public class PrivateAddress
    {
        private string city;
        private string country;
        private string streetAddress;
       
        public PrivateAddress() { }

        public PrivateAddress(string city, string country, string streetAddress)
        {
            this.city = city;
            this.country = country;
            this.streetAddress = streetAddress;
           
        }

        public PrivateAddress(string addressString)
        {
            //ulica broj,grad,drzava
            string[] strings = addressString.Split(',');
            city = strings[1];
            streetAddress = strings[0];
            country = strings[2];
        }

        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string StreetAddress { get => streetAddress; set => streetAddress = value; }

        public override string ToString()
        {
            return StreetAddress + "," + City + "," + Country;
        }
    }
}
