using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.User
{
    public class RegisterDTO
    {
        private string email;
        private string password;
        private string name;
        private string surname;
        private string address;
        private string city;
        private string state;
        private string phoneNum;
        private string gender;
        private string jmbg;
        private string bloodType;
        private string workplace;
        private string employmentInfo;

        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public string Jmbg { get { return jmbg; } set { jmbg = value;} }
        public string BloodType { get { return bloodType; } set { bloodType = value; } }
        public string Workplace { get { return workplace; } set { workplace = value; } }
        public string EmploymentInfo { get { return employmentInfo; } set { employmentInfo = value; } }
        public string City { get { return city; } set { city = value; } }
        public string State { get { return state; } set { state = value; } }
        public string PhoneNum { get { return phoneNum; } set { phoneNum = value; } }

    }
}
