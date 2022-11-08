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
        public class UserBuilder
        {
            public UserType userType;
            public int id;
            public string email;
            public string password;
            public string name;
            public string adress;
            public string phoneNumber;
            public string jmbg;
            public Gender gender;
            public string profession;
            public string workplace;
            public int idOfCenter;

            public UserBuilder IDOfCenter(int idOfCenter)
            {
                this.idOfCenter = idOfCenter;
                return this;
            }

            public UserBuilder Workplace(string workplace)
            {
                this.workplace = workplace;
                return this;
            }

            public UserBuilder Profession(string profession)
            {
                this.profession = profession;
                return this;
            }

            public UserBuilder Gender(Gender gender)
            {
                this.gender = gender;
                return this;
            }

            public UserBuilder JMBG(string jmbg)
            {
                this.jmbg = jmbg;
                return this;
            }

            public UserBuilder PhoneNumber(string phoneNumber)
            {
                this.phoneNumber = phoneNumber;
                return this;
            }

            public UserBuilder Adress(string adress)
            {
                this.adress = adress;
                return this;
            }

            public UserBuilder UserTemplate(UserType userType, int id, string email, string password, string name)
            {
                this.userType = userType;
                this.id = id;
                this.email = email;
                this.password = password;
                this.name = name;
                return this;
            }

            public User build()
            {
                User user = new User(this);
                return user;
            }
        }
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
        public User(UserBuilder builder)
        {
            this.userType = builder.userType;
            this.idOfCenter = builder.idOfCenter;
            this.id = builder.id;
            this.email = builder.email;
            this.password = builder.password;
            this.name = builder.name;
            this.adress = builder.adress;
            this.phoneNumber = builder.phoneNumber;
            this.jmbg = builder.jmbg;
            this.gender = builder.gender;
            this.profession = builder.profession;
            this.workplace = builder.workplace;
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
