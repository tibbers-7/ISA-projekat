using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IBloodCenterService
    {
        IEnumerable<BloodCenter> GetAll();
        BloodCenter GetById(int id);
        void Create(BloodCenter bloodCenter);
        void Update(BloodCenter bloodCenter);
        void Delete(BloodCenter bloodCenter);
    }
}
