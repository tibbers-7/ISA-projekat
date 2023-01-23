using System;
using System.IO;

namespace BloodBankLibrary.Core.Addresses
{
    public class CenterAddress
    {
        private int id;
        private string city;
        private string country;
        private string streetAddress;
        private int centerId;
        public CenterAddress()
        {

        }

        public CenterAddress(string city, string country, string streetAddress, int centerId)
        {
            this.city = city;
            this.country = country;
            this.streetAddress = streetAddress;
            this.centerId = centerId;

        }

        public CenterAddress(string addressString, int centerId)
        {
            //ulica broj,grad,drzava
            string[] strings = addressString.Split(',');
            city = strings[1];
            streetAddress = strings[0];
            country = strings[2];
            this.centerId = centerId;
        }

        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string StreetAddress { get => streetAddress; set => streetAddress = value; }

        public int CenterId { get => centerId;set => centerId = value; }

        public int Id { get=> id; set => id = value; }

        public override string ToString()
        {
            return StreetAddress + "," + City + "," + Country;
        }
    }
}
