﻿using System;
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
        private byte[]? qrCode;
        
        public Appointment() { }

        
        public Appointment(AppointmentDTO dto)
        {
            this.id=dto.Id;
            this.centerId = dto.CenterId;
            this.staffId = dto.StaffId;
            this.startDate = DateTime.Parse(dto.Date);
            this.duration = dto.Duration;
            this.DonorId = dto.DonorId;
            this.status = Enum.Parse<AppointmentStatus>(dto.Status.ToUpper());
            //startDate = DateTime.ParseExact(dto.Date, "yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            startDate = DateTime.Parse(dto.Date);
            this.qrCode = dto.QrCode;
        }

      

        public int StaffId { get => staffId; set => staffId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public int Duration { get => duration; set => duration = value; }
        public int DonorId { get; set; }
        public int CenterId { get => centerId; set => centerId = value; }
        public AppointmentStatus Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }
        public int? ReportId { get; set; }
        public byte[]? QrCode { get => qrCode; set => qrCode = value; }

        public string EmailInfo(string centerName,string staffName)
        {
            string res = "Your appointment is scheduled to happen at "+ startDate.ToString("dd.MM.yyyy. HH:mm")+","+
                         " at the "+centerName+"."+
                         "\nThe staff tasked with your appointment is "+staffName+".";
            return res;
        }
    }
}