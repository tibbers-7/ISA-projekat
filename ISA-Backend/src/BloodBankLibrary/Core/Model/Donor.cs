using BloodBankLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class Donor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Jmbg { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }
        public Gender Gender { get; set; }

        public UserType userType { get; set; }


        //broj penala - nepojavljivanje na pregledu
        public int Strikes { get; set; }

        public Donor() { }
    }
}
