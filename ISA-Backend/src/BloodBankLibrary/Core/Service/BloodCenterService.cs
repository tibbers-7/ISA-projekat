using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public class BloodCenterService : IBloodCenterService
    {
        private readonly IBloodCenterRepository _bloodCenterRepository;

        public BloodCenterService(IBloodCenterRepository bloodCenterRepository)
        {
            _bloodCenterRepository = bloodCenterRepository;
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
    }
}
