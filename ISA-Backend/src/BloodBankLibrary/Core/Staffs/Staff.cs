using BloodBankLibrary.Core.Materials;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Users;
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
        [Column(TypeName = "jsonb")]
        public string AddressJson { get; set; }
        [NotMapped]
        private Address address { get; set; }
        [NotMapped]
        public Address Address
        {
            get => address; set
            {
                address = value;
                AddressJson = JsonSerializer.Serialize(address);
            }
        }
        public Gender Gender { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }
        public long PhoneNumber { get; set; }

        public Staff() { }

        public Staff(RegisterDTO regDTO)
        {
            this.Name = regDTO.Name;
            this.Surname = regDTO.Surname;
            this.Email = regDTO.Email;
            this.Address = new Address() { City = regDTO.City, Country = regDTO.State, StreetAddr = regDTO.Address };
            this.AddressJson = JsonSerializer.Serialize(this.Address);
            this.Jmbg = regDTO.Jmbg;
            Gender g;
            Gender.TryParse(regDTO.Gender, out g);
            this.Gender = g;
            // BloodType.TryParse(regDTO.BloodType, out this.bloodType);
            this.Profession = regDTO.EmploymentInfo;
            this.Workplace = regDTO.Workplace;
            this.PhoneNumber = regDTO.PhoneNum;
        }
    }
}
