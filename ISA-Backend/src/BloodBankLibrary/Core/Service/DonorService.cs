using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;

        public DonorService(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public IEnumerable<Donor> GetAll()
        {
            return _donorRepository.GetAll();
        }

        public Donor GetById(int id)
        {
            return _donorRepository.GetById(id);
        }

        public void Create(Donor donor)
        {
            _donorRepository.Create(donor);
        }

        public void Update(Donor donor)
        {
            _donorRepository.Update(donor);
        }

        public void Delete(Donor donor)
        {
            _donorRepository.Delete(donor);
        }
    }
}
