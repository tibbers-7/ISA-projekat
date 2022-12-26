using System;
using System.ComponentModel.DataAnnotations;
using BloodBankLibrary.Core.Materials.Enums;

namespace BloodBankLibrary.Core.Appointments
{
    public class Appointment
    {
        [Key]
        private int id;
        private int staffId;
        private DateTime startDate;
        private int duration;
        private int centerId;
        private AppointmentStatus status;
        
        public Appointment() { }

        
        public Appointment(AppointmentDTO dto)
        {
            this.centerId = dto.CenterId;
            this.staffId = dto.StaffId;
            this.startDate = DateTime.Parse(dto.Date);
            this.duration = dto.Duration;
            this.DonorId = dto.DonorId;
            this.status = Enum.Parse<AppointmentStatus>(dto.Status);
        }

      

        public int StaffId { get => staffId; set => staffId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get; set; }
        public int CenterId { get => centerId; set => centerId = value; }
        public AppointmentStatus Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }
        public int? ReportId { get; set; }
    }
}