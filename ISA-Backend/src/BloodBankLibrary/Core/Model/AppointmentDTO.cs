using BloodBankLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Model
{
    public class AppointmentDTO
    {
        private int id;
        private int staffId;
        private int donorId;
        private string date;
        private int duration;
        private int centerId;

      
        public AppointmentDTO(int id, int duration, int staffId, int donorId, string date, int centerId)
        {
            this.id = id;
            this.staffId = staffId;
            this.donorId = donorId;
            this.date = date;
            this.duration = duration;
            this.centerId = centerId;
            
        }

        public int StaffId { get => staffId; set => staffId = value; }
        public string Date { get => date; set => date = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get => donorId; set => donorId = value; }
        public int CenterId { get => centerId; set => centerId = value; }
        public int Id { get => id; set => id = value; }
    }
}
