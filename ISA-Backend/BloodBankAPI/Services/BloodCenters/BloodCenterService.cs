using System.Collections.Generic;
using System.Text.Json;

namespace BloodBankAPI.Services.BloodCenters
{
    public class BloodCenterService : IBloodCenterService
    {
        private readonly IBloodCenterRepository _bloodCenterRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IAddressRepository _addressRepository;

        public BloodCenterService(IBloodCenterRepository bloodCenterRepository, IAppointmentRepository appointmentRepository, IDonorRepository donorRepository, IAddressRepository addressRepository)
        {
            _bloodCenterRepository = bloodCenterRepository;
            _appointmentRepository = appointmentRepository;
            _donorRepository = donorRepository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<CenterDTO> GetAll()
        {
            List<CenterDTO> result = new List<CenterDTO>();
            foreach (BloodCenter center in _bloodCenterRepository.GetAll())
            {
                CenterAddress addr = _addressRepository.GetByCenter(center.Id);
                CenterDTO dto = new CenterDTO(center);
                dto.stringAddress = addr.StreetAddress + "," + addr.City + "," + addr.Country;
                result.Add(dto);
            }
            return result;
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

        public IEnumerable<CenterDTO> GetSearchResult(string content)
        {
            if (content == "") return GetAll();
            List<CenterDTO> res = new List<CenterDTO>();

            foreach (CenterDTO center in GetAll())
            {
                if (center.Name.ToLower().Contains(content.ToLower()) || center.stringAddress.ToLower().Contains(content.ToLower()))
                {

                    res.Add(center);
                }

            }

            return res;
        }
    }
}
