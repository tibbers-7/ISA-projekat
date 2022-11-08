using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Repository
{
    public interface IBloodCenterRepository
    {
        IEnumerable<BloodCenter> GetAll();
        BloodCenter GetById(int id);
        void Create(BloodCenter bloodCenter);
        void Update(BloodCenter bloodCenter);
        void Delete(BloodCenter bloodCenter);
    }
}
