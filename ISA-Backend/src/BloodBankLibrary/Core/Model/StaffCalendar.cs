using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class StaffCalendar
    {
        int StaffId { get; set; }
        List<Appointment> Appoinments { get; set; }

        public StaffCalendar() { }
    }
}
