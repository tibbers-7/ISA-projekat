using BloodBankAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankAPI.Materials.DTOs
{
    public class CenterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OpenHours { get; set; }
        public double AvgScore { get; set; }
        public string stringAddress { get; set; }

        /*
        public CenterDTO(BloodCenter center)
        {
            Id=center.Id;
            Name = center.Name;
            Description = center.Description;
            AvgScore = center.AvgScore;
            OpenHours=center.WorkTimeStart.ToString("HH:mm") + " - "+center.WorkTimeEnd.ToString("HH:mm");
        }
        */
        public CenterDTO() { }
    }
}
