using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BloodBankLibrary.Core.Model
{
    public class Address
    {
        private string city;
        private string country;
        private string number;
        private string street;

        public Address()
        {

        }

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
            this.city = strings[2];
            this.street = strings[0];
            this.number = strings[1];
            this.country = strings[3];
        }

        public string City { get => city; set => city = value; }
        public string Country { get => country; set => country = value; }
        public string Number { get => number; set => number = value; }
        public string Street { get => street; set => street = value; }
        public override string ToString()
        {
            return Street + " " + Number + " " + City + " " + Country;
        }
    }
}
