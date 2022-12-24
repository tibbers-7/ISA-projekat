using System.Collections.Generic;

namespace BloodBankLibrary.Core.Centers
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
