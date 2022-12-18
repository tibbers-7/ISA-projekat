﻿using BloodBankLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public UserType UserType { get; set; }

        public Admin() { }
    }
}
