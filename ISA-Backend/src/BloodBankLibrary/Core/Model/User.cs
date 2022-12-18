using BloodBankLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class User
    {
        [Key]
        private int id;
        private int idByType;
        private string email;
        private string password;
        private string name;
        private string surname;
        private UserType userType;

        public User(int id, int idByType, string email, string password, string name, string surname, UserType userType)
        {
            this.id = id;
            this.idByType = idByType;
            this.email = email;
            this.password = password;
            this.name = name;
            this.surname = surname;
            this.userType = userType;
        }


        public User() { }

        public int Id { get => id; set => id = value; }
        public int IdByType { get => idByType; set => idByType = value; }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

        public UserType UserType { get => userType; set => userType = value; }

    }

}
