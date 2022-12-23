using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IDonorService
    {
        IEnumerable<Donor> GetAll();
        Donor GetById(int id);
        void Register(Donor donor);
        void Update(Donor donor);
        void Delete(Donor donor);
        Donor GetByEmail(string email);
        void AddStrike(int donorId);
    }
}
