﻿using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.Enums;

namespace BloodBankAPI.Model
{
    public class Account : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserType UserType {get;set;}

        public string Token { get; set; }

        public bool IsActive { get; set; }


        public Account() { }

    }
}
