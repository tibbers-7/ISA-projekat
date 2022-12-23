using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System.Collections.Generic;

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


        public void Register(Donor donor)
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

        public Donor GetByEmail(string email)
        {
            foreach (Donor d in GetAll())
            {
                if (d.Email.Equals(email)) return d;
            }
            return null;

        }

        public void AddStrike(int donorId)
        {
            Donor d=GetById(donorId);
            d.Strikes++;
            Update(d);
        }
    }
}
