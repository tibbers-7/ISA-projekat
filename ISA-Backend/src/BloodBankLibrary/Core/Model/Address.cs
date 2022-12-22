using System;

namespace BloodBankLibrary.Core.Model
{
    public class Address
    {
        private string city;
        private string country;
        private string streetAddr;

        public Address()
        {

        }

        public Address(string city, string country, string streetAddr)
        {
            this.city = city;
            this.country = country;
            this.streetAddr = streetAddr;
        }
        public Address(string addressString)
        {
            String[] strings=addressString.Split(',');
            this.city = strings[1];
            this.streetAddr = strings[0];
            this.country = strings[2];
        }

        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string StreetAddr { get => streetAddr; set => streetAddr = value; }
        public override string ToString()
        {
            return StreetAddr + " " + City + " " + Country;
        }
    }
}
