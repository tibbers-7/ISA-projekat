﻿using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Admins
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }


        public Admin() { }

        public Admin(int Id, string Email, string Name, string Surname)
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
            this.Surname = Surname;
        }
    }
}