using BloodBankAPI.Materials.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankAPI.Model
{
    public class Profile : Entity
    {
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

        //broj penala - nepojavljivanje na pregledu
        public int Strikes { get; set; }
        public int CenterId { get; set; }
    }
}
