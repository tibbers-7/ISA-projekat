using BloodBankLibrary.Core.Addresses;
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
        [NotMapped]
        private PrivateAddress privateAddress { get; set; }

        [NotMapped]
        public PrivateAddress Address
        {
            get
            {
                return new PrivateAddress(AddressString);
            }
            set => privateAddress = value;
        }


        public long PhoneNumber { get; set; }
        public long Jmbg { get; set; }
        public string Profession { get; set; }
        public string Workplace { get; set; }
        public Gender Gender { get; set; }
       
        public string AddressString { get; set; }

        [NotMapped]
        public string Password { get; set; } //ovo sluzi samo za update, nakon sto se i user updatuje polje se anulira
        
        //broj penala - nepojavljivanje na pregledu
        public int Strikes { get; set; }

        public Donor(RegisterDTO regDTO)
        {
            this.Name = regDTO.Name;
            this.Surname = regDTO.Surname;
            this.Email = regDTO.Email;
            this.Address = new PrivateAddress(regDTO.City, regDTO.State, regDTO.Address);
            this.Jmbg = regDTO.Jmbg;
            Gender.TryParse(regDTO.Gender, out Gender g);
            this.Gender = g;
            // BloodType.TryParse(regDTO.BloodType, out this.bloodType);
            this.Profession = regDTO.EmploymentInfo;
            this.Workplace = regDTO.Workplace;
            this.PhoneNumber = regDTO.PhoneNum;
        }

        public Donor() { }
    }
}
