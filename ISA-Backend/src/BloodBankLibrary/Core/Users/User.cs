using BloodBankLibrary.Core.Materials.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Users
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
        private string token;
        private bool active;

        

        public User(RegisterDTO regDTO, int roleID) //samo pacijent moze da se registruje
        {
            this.UserType = UserType.DONOR;
            this.idByType = roleID;
            this.name = regDTO.Name;
            this.surname = regDTO.Surname;
            this.email = regDTO.Email;
            this.password = regDTO.Password;
        }


        public User() { }

        public int Id { get => id; set => id = value; }
        public int IdByType { get => idByType; set => idByType = value; }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }

        public UserType UserType { get => userType; set => userType = value; }
        public string Token { get => token; set => token = value; }
        public bool Active { get => active; set => active = value; }


    }

}
