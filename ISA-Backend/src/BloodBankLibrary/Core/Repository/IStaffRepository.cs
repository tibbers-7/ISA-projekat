using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAll();
        Staff GetById(int id);
        void Create(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
    }
}
