using System.Collections.Generic;

namespace BloodBankLibrary.Core.Donors
{
    public interface IDonorRepository
    {
        IEnumerable<Donor> GetAll();
        Donor GetById(int id);
        void Create(Donor donor);
        void Update(Donor donor);
        void Delete(Donor donor);
        Donor GetByEmail(string email);
    }
}
