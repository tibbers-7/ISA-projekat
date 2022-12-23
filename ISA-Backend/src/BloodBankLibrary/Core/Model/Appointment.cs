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
        public Appointment(int id, int duration, int staffId, DateTime startDate, int centerId)
        {
            this.id = id;
            this.staffId = staffId;
            this.startDate = startDate;
            this.duration = duration;
            this.centerId = centerId;
            this.status = AppointmentStatus.AVAILABLE;
            donorId = -1;
        }

        //kad pravi korisnik
        public Appointment(int id, int duration, int staffId, int donorId, DateTime startDate, int centerId)
        {
            this.id = id;
            this.staffId = staffId;
            this.donorId = donorId;
            this.startDate = startDate;
            this.duration = duration;
            this.centerId = centerId;
            this.status = AppointmentStatus.SCHEDULED;
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