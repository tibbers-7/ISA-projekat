using BloodBankLibrary.Core.Model;
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
