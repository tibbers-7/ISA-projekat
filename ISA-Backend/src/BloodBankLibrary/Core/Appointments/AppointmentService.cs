﻿using System;
using System.Collections.Generic;
using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Donors;
using BloodBankLibrary.Core.EmailSender;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Materials.QRGenerator;
using BloodBankLibrary.Core.Staffs;

namespace BloodBankLibrary.Core.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBloodCenterRepository _bloodCenterRepository;


        private readonly IQRService _qRService;
        private readonly IBloodCenterService _centerService;
        private readonly IStaffService _staffService;
        private readonly IEmailSendService _emailSendService;
        private readonly IDonorService _donorService;
        public AppointmentService(IAppointmentRepository appointmentRepository, IBloodCenterRepository bloodCenterRepository,
                                    IQRService qRService,
                                    IBloodCenterService centerService,
                                    IStaffService staffService,
                                    IEmailSendService emailSendService,
                                    IDonorService donorService)
        {
            _appointmentRepository = appointmentRepository;
            _bloodCenterRepository = bloodCenterRepository;

            _qRService = qRService;
            _centerService = centerService;
            _staffService = staffService;
            _emailSendService = emailSendService;
            _donorService = donorService;

        }


        public void Create(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
        }


        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public void Update(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public IEnumerable<Appointment> GetAvailable()
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            //svi koji su available i u buducnosti, mada mozda i neko brisnanje da se napravi za available al ne mora
            List<Appointment> available = new List<Appointment>();
            foreach(Appointment appointment in allAppointments)
            {
                if(appointment.Status == AppointmentStatus.AVAILABLE || appointment.Status == AppointmentStatus.CANCELLED)
                {
                    if (DateTime.Compare(appointment.StartDate, DateTime.Now) > 0) available.Add(appointment);
                  
                }
            }
            return available;
           
        }

        public IEnumerable<Appointment> GetScheduled()
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            //svi scheduled koji su u buducnosti ili nsiu jos zavrseni
            List<Appointment> res = new List<Appointment>();
           // return allAppointments.Where<Appointment>(a => a.Status == AppointmentStatus.SCHEDULED && DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0);
           foreach(Appointment appointment in allAppointments)
            {
                if (appointment.Status==AppointmentStatus.SCHEDULED) {
                    if (DateTime.Compare(appointment.StartDate.AddMinutes(appointment.Duration), DateTime.Now) >= 0)
                    {
                        res.Add(appointment);
                    }
                }
            }
            return res;
        }



        public IEnumerable<AppointmentDTO> GetScheduledByDonor(int donorId)
        {
            IEnumerable<Appointment> allScheduled = GetScheduled();
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach(Appointment appointment in allScheduled)
            {
                if (appointment.DonorId == donorId) res.Add(new AppointmentDTO(appointment));
            }
            return res;
            //return allScheduled.Where<Appointment>(a => a.DonorId == donorId);
        }

        public IEnumerable<Appointment> GetScheduledByCenter(int id)
        {
            IEnumerable<Appointment> allScheduled = GetScheduled();
            List<Appointment> res = new List<Appointment>();
           foreach(Appointment appointment in allScheduled)
            {
                if(appointment.CenterId == id) res.Add(appointment) ;
            }
            return res;
        }

        public IEnumerable<Appointment> GetAvailableByCenter(int id)
        {
            IEnumerable<Appointment> allAvailable = GetAvailable();
            List<Appointment> res = new List<Appointment>();
            foreach (Appointment appointment in allAvailable)
            {
                if (appointment.CenterId == id) res.Add(appointment);
            }
            return res;
        }

        //ovo popraviti da bude buducnost ako treba
        public ICollection<Appointment> GetByStaffId(int id)
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            List<Appointment> selectedAppointments = new List<Appointment>();
            foreach (Appointment a in allAppointments)
            {
                if (a.StaffId == id)
                {
                    selectedAppointments.Add(a);
                }
            }

            return selectedAppointments;
        }

        public IEnumerable<BloodCenter> GetCentersForDateTime(string dateTime)
        {
            //ovo parsiranje cu srediti kad sredim front
            DateTime parsedDateTime = DateTime.Parse(dateTime);
            IEnumerable<BloodCenter> bloodCenters = _bloodCenterRepository.GetAll();
            List<BloodCenter> availableCenters = new List<BloodCenter>();
           
            foreach(BloodCenter center in bloodCenters)
            {
               if(CheckIfCenterAvailable(center.Id, parsedDateTime, 30))
                {
                    availableCenters.Add(center);
                }
            }
            return availableCenters;
        }

        public bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration)
        {
            List<Appointment> allCenterApps = (List<Appointment>)GetScheduledByCenter(centerId);
            allCenterApps.AddRange(GetAvailableByCenter(centerId));
            BloodCenter bloodCenter = _bloodCenterRepository.GetById(centerId);
            if (dateTime.Hour < bloodCenter.WorkTimeStart.Hour || dateTime.Hour > bloodCenter.WorkTimeEnd.Hour) return false;
            foreach (Appointment app in allCenterApps)
            {
                
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), dateTime, dateTime.AddMinutes(duration)))
                {
                    return false;
                }

             }
            return true;

        }

        public bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            //ako je pocetak prvog pre pocetka drugog, onda prvi mora i da bude zavrsen pre nego sto drugi pocne
            if (DateTime.Compare(start1, start2) < 0)
            {
                if (DateTime.Compare(end1, start2) > 0) return true;
                else return false;
            }
            //ako je pocetak drugog pre pocetka prvog, onda kraj drugog mora biti pre pocetka prvog
            else if(DateTime.Compare(start2,start1)<0)
            {
                if (DateTime.Compare(end2, start1) > 0) return true;
                else return false;
            }
            //ne mogu da pocnu u isto vreme
            return true;

        }

        public Appointment CancelAppt(int apptId)
        {
            
            Appointment appt=GetById(apptId);
            DateTime cancelBy = appt.StartDate.AddDays(1);
            if (DateTime.Compare(cancelBy, DateTime.Now) < 0) return appt;
            return null;

        }

        public object GetAvailableForDonor(int donorId, int centerId)
        {
            IEnumerable<Appointment> appointments = GetAvailableByCenter(centerId);
            List<AppointmentDTO> res = new List<AppointmentDTO>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Status == AppointmentStatus.CANCELLED && appointment.DonorId == donorId) continue;
                res.Add(new AppointmentDTO(appointment));
            }
            return res;
        }

        public Appointment GenerateAndSaveQR(Appointment appointment)
        {
            Staff staff = _staffService.GetById(appointment.StaffId);

            string filePath=appointment.DonorId.ToString()+"_"+appointment.CenterId.ToString()+"_"+appointment.StartDate.ToString("dd_MM_yyyy_HH_mm")+".jpg";

            byte[] qr = _qRService.GenerateQR(appointment.EmailInfo(
                                                         _centerService.GetById(appointment.CenterId).Name,
                                                         staff.Name +" "+ staff.Surname),filePath);
            appointment.QrCode = qr;
            Donor donor = _donorService.GetById(appointment.DonorId);
            string subject = "BloodCenter - Scheduled appointment information";
            string body = "Here is the QR code with your information:\n";

            filePath = "AppData\\" + filePath;

            _emailSendService.SendWithQR(new Message(new string[] { donor.Email }, subject, body), qr,filePath);
            //_qRService.DeleteImage(filePath);

            return appointment;
            
        }

        public object GetAllByDonor(int id)
        {
            List<AppointmentDTO> res=new List<AppointmentDTO>();
            foreach(Appointment appointment in GetAll()) {
                if(appointment.DonorId == id) 
                    res.Add(new AppointmentDTO(appointment));

            }

            return res;
        }
    }
}