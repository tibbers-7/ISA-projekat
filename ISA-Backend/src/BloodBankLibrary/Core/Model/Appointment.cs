using System;

namespace BloodBankLibrary.Core.Model
{
    public class Appointment
    {
        int staffId;
        DateTime date;
        int duration;
        int centerId;

        public Appointment(int staffId, DateTime date, int duration, int centerId)
        {
            this.staffId = staffId;
            this.date = date;
            this.duration = duration;
            this.centerId = centerId;
        }

        public int StaffId { get => staffId; set => staffId = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Duration { get => duration; set => duration = value; }
        public int CenterId { get => centerId; set => centerId = value; }
    }
}