﻿using System.Collections.Generic;

namespace BloodBankLibrary.Core.Donors
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
            return _donorRepository.GetByEmail(email);

        }

        public void AddStrike(int donorId)
        {
            Donor d=GetById(donorId);
            d.Strikes++;
            Update(d);
        }
    }
}
