using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Model.Enums;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
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

        public ICollection<Appointment> GetByCenterId(int id)
        {
            IEnumerable <Appointment> allAppointments = _appointmentRepository.GetAll();
            List < Appointment > selectedAppointments= new List<Appointment>();
            foreach(Appointment a in allAppointments)
            {
                if(a.CenterId == id)
                {
                    selectedAppointments.Add(a);
                }
            }

            return selectedAppointments;
           
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
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

        public void Update(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
        }
    }
}
