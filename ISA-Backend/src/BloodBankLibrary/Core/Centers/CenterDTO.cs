using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Centers
{
    public class CenterDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string OpenHours { get; set; }
        public double AvgScore { get; set; }

        public CenterDTO(BloodCenter center)
        {
            Name = center.Name;
            Description = center.Description;
            Address = center.Address.ToString();
            AvgScore = center.AvgScore;
            OpenHours=center.WorkTimeStart.ToString("HH:mm") + " - "+center.WorkTimeEnd.ToString("HH:mm");
        }
    }
}
