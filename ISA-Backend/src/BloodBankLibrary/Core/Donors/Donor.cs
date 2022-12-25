using BloodBankLibrary.Core.Materials;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace BloodBankLibrary.Core.Donors
{
    public class Donor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Column(TypeName = "jsonb")]
        public string AddressJson { get; set;}
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
        public long PhoneNumber { get; set; }
        public long Jmbg { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }
        public Gender Gender { get; set; }



        //broj penala - nepojavljivanje na pregledu
        public int Strikes { get; set; }

        public Donor(RegisterDTO regDTO)
        {
            this.Name = regDTO.Name;
            this.Surname = regDTO.Surname;
            this.Email = regDTO.Email;
            this.Address = new Address() { City=regDTO.City, Country=regDTO.State,StreetAddr=regDTO.Address};
            this.AddressJson=JsonSerializer.Serialize(this.Address);
            this.Jmbg = regDTO.Jmbg;
            Gender g;
            Gender.TryParse(regDTO.Gender, out g);
            this.Gender = g;
            // BloodType.TryParse(regDTO.BloodType, out this.bloodType);
            this.Profession = regDTO.EmploymentInfo;
            this.Workplace = regDTO.Workplace;
            this.PhoneNumber = regDTO.PhoneNum;
        }

        public Donor() { }
    }
}
