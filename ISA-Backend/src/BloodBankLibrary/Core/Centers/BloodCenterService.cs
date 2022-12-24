using System.Collections.Generic;
using BloodBankLibrary.Core.Appointments;
using BloodBankLibrary.Core.Donors;

namespace BloodBankLibrary.Core.Centers
{
    public class BloodCenterService : IBloodCenterService
    {
        private readonly IBloodCenterRepository _bloodCenterRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDonorRepository _donorRepository; 

        public BloodCenterService(IBloodCenterRepository bloodCenterRepository, IAppointmentRepository appointmentRepository, IDonorRepository donorRepository)
        {
            _bloodCenterRepository = bloodCenterRepository;
            _appointmentRepository = appointmentRepository;
            _donorRepository = donorRepository;
        }

        public IEnumerable<BloodCenter> GetAll()
        {
            return _bloodCenterRepository.GetAll();
        }

        public BloodCenter GetById(int id)
        {
            return _bloodCenterRepository.GetById(id);
        }

        public void Create(BloodCenter bloodCenter)
        {
            _bloodCenterRepository.Create(bloodCenter);
        }

        public void Update(BloodCenter bloodCenter)
        {
            _bloodCenterRepository.Update(bloodCenter);
        }

        public void Delete(BloodCenter bloodCenter)
        {
            _bloodCenterRepository.Delete(bloodCenter);
        }

        public IEnumerable<Donor> GetDonorsByCenterId(int centerId)
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAll();
            List<Donor> donors = new List<Donor>();
            foreach (Appointment appointment in allAppointments)
            {
                //dodati proveru da bude completed al onda napraviti da kad se zavrsi da se status promeni u completed
                if(appointment.CenterId == centerId)
                {
                    donors.Add(_donorRepository.GetById(appointment.DonorId));
                }
                
            }

            return donors;
        }

        
    }
}
