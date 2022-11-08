using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class User
    {
        UserType userType;
        int idOfCenter;
        int id;
        string email;
        string password;
        string name;
        string adress;
        string phoneNumber;
        string jmbg;
        Gender gender;
        string profession;
        string workplace;

        public User(UserType userType, int idOfCenter, int id, string email,
            string password, string name, string adress, string phoneNumber, 
            string jmbg, Gender gender, string profession, string workplace)
        {
            this.userType = userType;
            this.idOfCenter = idOfCenter;
            this.id = id;
            this.email = email;
            this.password = password;
            this.name = name;
            this.adress = adress;
            this.phoneNumber = phoneNumber;
            this.jmbg = jmbg;
            this.gender = gender;
            this.profession = profession;
            this.workplace = workplace;
        }

        public int IdOfCenter { get => idOfCenter; set => idOfCenter = value; }
        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Adress { get => adress; set => adress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string Profession { get => profession; set => profession = value; }
        public string Workplace { get => workplace; set => workplace = value; }
        internal UserType UserType { get => userType; set => userType = value; }
        internal Gender Gender { get => gender; set => gender = value; }
    }
}
