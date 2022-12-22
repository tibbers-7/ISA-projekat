﻿using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }


        public Admin() { }
    }
}
