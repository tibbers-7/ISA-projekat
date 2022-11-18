using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodBankLibrary.Core.Model
{
    internal class Address
    {
        String city;
        String country;
        String number;
        String street;

        public Address(string city, string country, string number, string street)
        {
            this.city = city;
            this.country = country;
            this.number = number;
            this.street = street;
        }
        public Address(string addressString)
        {
            String[] strings=addressString.Split(' ');
            this.city = strings[0];
            this.street = strings[1];
            this.number = strings[2];
            this.country = strings[3];
        }

        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string Number { get => number; set => number = value; }
        public string Street { get => street; set => street = value; }
        public override string ToString()
        {
            return City + " " + Street + " " + Number + " " + Country;
        }
    }
}
