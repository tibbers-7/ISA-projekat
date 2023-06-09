using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankAPI.Model
{
    public class Appointment : Entity
    {
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public AppointmentStatus Status { get; set; }
        public int? ReportId { get; set; }
        public byte[]? QrCode { get; set; }
        public int CenterId { get; set; }
        public int DonorId { get; set; }
        public int StaffId { get; set; }
        public virtual BloodCenter Center { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Staff Staff { get; set; }
     
        public Appointment() { }

        /*
        public Appointment(AppointmentDTO dto)
        {
            Id=dto.Id;
            StartDate = DateTime.Parse(dto.StartDate);
            Duration = dto.Duration;
            Status = Enum.Parse<AppointmentStatus>(dto.Status.ToUpper());
            //startDate = DateTime.ParseExact(dto.Date, "yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            QrCode = dto.QrCode;
        }
        */

        public string EmailInfo(string centerName,string staffName,string cancelReason)
        {
            string res="";
            if (Status == AppointmentStatus.SCHEDULED)
            {
                res = "Your appointment is scheduled to happen at " + StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + centerName + "." +
                             "\nThe staff tasked with your appointment is " + staffName + ".";
                
            }
            else if (Status == AppointmentStatus.CANCELLED)
            {       
                res = "Your appointment that was supposed to happen at " + StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + centerName + ", with staff " + staffName + ", because " + cancelReason;
            }
            else if (Status == AppointmentStatus.COMPLETED)
            {
                res = "Your appointment at " + StartDate.ToString("dd.MM.yyyy. HH:mm") + "," +
                             " at the " + centerName + ", with staff " + staffName + " was completed.";
            }
            return res;
        }
    }
}