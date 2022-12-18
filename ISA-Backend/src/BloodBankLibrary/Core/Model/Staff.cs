using BloodBankLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class Staff
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public int CenterId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public UserType UserType { get; set; }

        public StaffCalendar Calendar { get; set;}

        public Staff() { }
    }
}
