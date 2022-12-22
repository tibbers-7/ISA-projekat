using BloodBankLibrary.Core.Model.Enums;
using BloodBankLibrary.Core.User;

namespace BloodBankLibrary.Core.Model
{
    public class Donor
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Jmbg { get; set; }
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
            this.Jmbg = regDTO.Jmbg;
            Gender g;
            Gender.TryParse(regDTO.Gender, out g);
            this.Gender = g;
            // BloodType.TryParse(regDTO.BloodType, out this.bloodType);
            this.Profession = regDTO.EmploymentInfo;
            this.Workplace = regDTO.Workplace;
        }

        public Donor() { }
    }
}
