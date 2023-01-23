using BloodBankLibrary.Core.Addresses;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Users;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BloodBankLibrary.Core.Staffs
{
    public class Staff
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public int CenterId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public long Jmbg { get; set; }

        [NotMapped]
        public PrivateAddress Address
        {
            get
            {
                return new PrivateAddress(AddressString);
            }
            set
            {
                Address = value;
                AddressString = Address.ToString();
            }
        }

        public Gender Gender { get; set; }
        public long PhoneNumber { get; set; }
      
        public string AddressString { get; set; }

        public Staff() { }

        public Staff(RegisterDTO regDTO)
        {
            this.Name = regDTO.Name;
            this.Surname = regDTO.Surname;
            this.Email = regDTO.Email;
            this.Address = new PrivateAddress() { City = regDTO.City, Country = regDTO.State, StreetAddress = regDTO.Address };
            this.Jmbg = regDTO.Jmbg;
            this.CenterId = regDTO.IdOfCenter;
            Gender.TryParse(regDTO.Gender, out Gender g);
            this.Gender = g;
            this.PhoneNumber = regDTO.PhoneNum;
        }

        

    }
}
