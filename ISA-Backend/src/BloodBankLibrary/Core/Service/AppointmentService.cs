using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Model.Enums;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IBloodCenterRepository _bloodCenterRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IBloodCenterRepository bloodCenterRepository)
        {
            _appointmentRepository = appointmentRepository;
            _bloodCenterRepository = bloodCenterRepository;

        }


        public void Create(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
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
            return allAppointments.Where<Appointment>(a => a.Status == AppointmentStatus.AVAILABLE && DateTime.Compare(a.StartDate, DateTime.Now) > 0);
           
        }

        public IEnumerable<Appointment> GetScheduled()
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            //svi scheduled koji su u buducnosti ili nsiu jos zavrseni
            return allAppointments.Where<Appointment>(a => a.Status == AppointmentStatus.SCHEDULED && DateTime.Compare(a.StartDate.AddMinutes(a.Duration), DateTime.Now) >= 0);
           
        }

        public IEnumerable<Appointment> GetCancelled()
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            return allAppointments.Where<Appointment>(a => a.Status == AppointmentStatus.CANCELLED);
          
        }

        public IEnumerable<Appointment> GetCompleted()
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            
           return allAppointments.Where<Appointment>(a => a.Status == AppointmentStatus.COMPLETED);
          
        }

        public IEnumerable<Appointment> GetScheduledByDonor(int donorId)
        {
            IEnumerable<Appointment> allScheduled = GetScheduled();
            return allScheduled.Where<Appointment>(a => a.DonorId == donorId);
        }

        public IEnumerable<Appointment> GetScheduledByCenter(int id)
        {
            IEnumerable<Appointment> allScheduled = GetScheduled();
            return allScheduled.Where<Appointment>(a => a.CenterId == id);
        }

        public IEnumerable<Appointment> GetAvailableByCenter(int id)
        {
            IEnumerable<Appointment> allAvailable = GetAvailable();
            return allAvailable.Where<Appointment>(a => a.CenterId == id);
        }

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
               if(CheckIfCenterAvailable(center.Id, parsedDateTime))
                {
                    availableCenters.Add(center);
                }
            }
            return availableCenters;
        }

        public bool CheckIfCenterAvailable(int centerId, DateTime dateTime)
        {
            IEnumerable<Appointment> allCenterApps = GetScheduledByCenter(centerId);
            foreach (Appointment app in allCenterApps)
            {
                //ako ima preklapanja centar nije slobodan izlazimo iz petlje
                if (Overlaps(app.StartDate, app.StartDate.AddMinutes(app.Duration), dateTime, dateTime.AddMinutes(30)))
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


       
       
    }
}
