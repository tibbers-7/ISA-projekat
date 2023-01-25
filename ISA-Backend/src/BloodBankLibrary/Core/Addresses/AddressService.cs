using BloodBankLibrary.Core.Appointments;
using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Addresses
{
    public class AddressService: IAddressService
    {
        private readonly IAddressRepository _addressRepository;
       
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public IEnumerable<CenterAddress> GetAll()
        {
            return _addressRepository.GetAll();
        }

        public IEnumerable<string> GetCities()
        {
            List<string> cities = new List<string>();
            foreach (CenterAddress address in _addressRepository.GetAll())
            {
                if (!cities.Contains(address.City)) cities.Add(address.City);
            }
            return cities;
        }

        public CenterAddress GetById(int id)
        {

            return _addressRepository.GetById(id);
        }

        public CenterAddress GetByCenter(int centerId)
        {
            foreach(CenterAddress address in _addressRepository.GetAll()) { 
                if(address.CenterId == centerId) return address;
            }
            return null;
        }

        public void Create(CenterAddress address)
        {
            _addressRepository.Create(address);
        }

        public void Update(CenterAddress address)
        {
            _addressRepository.Update(address);
        }

        public void Delete(CenterAddress address)
        {
            _addressRepository.Delete(address);
        }

        
    }
}
