using System.Collections.Generic;
using System.Text.Json;
using BloodBankLibrary.Core.Appointments;
using BloodBankLibrary.Core.Donors;
using BloodBankLibrary.Core.Materials;

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

        public IEnumerable<CenterDTO> GetAll()
        {
            List<CenterDTO> result = new List<CenterDTO>();
            foreach(BloodCenter center in _bloodCenterRepository.GetAll())
            {
                result.Add(new CenterDTO(center));
            }
            return result;
        }

        public IEnumerable<string> GetCities()
        {
            List<string> cities = new List<string>();
            foreach(BloodCenter center in _bloodCenterRepository.GetAll())
            {
                if(!cities.Contains(center.Address.City)) cities.Add(center.Address.City);
            }
            return cities;
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
