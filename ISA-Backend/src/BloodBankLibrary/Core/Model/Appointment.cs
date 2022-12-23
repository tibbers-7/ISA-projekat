using System;
using BloodBankLibrary.Core.Model.Enums;

namespace BloodBankLibrary.Core.Model
{
    public class Appointment
    {
        private int id;
        private int staffId;
        private int donorId;
        private DateTime startDate;
        private int duration;
        private int centerId;
        private AppointmentStatus status;
        
        public Appointment() { }

        //kad staff pravi
        public Appointment(AppointmentDTO dto)
        {
            this.centerId = dto.CenterId;
            this.staffId = dto.StaffId;
            this.startDate = DateTime.Parse(dto.Date);
            this.duration = dto.Duration;
            this.donorId = dto.DonorId;
        }

      

        public int StaffId { get => staffId; set => staffId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get => donorId; set => donorId = value; }
        public int CenterId { get => centerId; set => centerId = value; }
        public AppointmentStatus Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }
    }
}