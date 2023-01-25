using System.Collections.Generic;

namespace BloodBankLibrary.Core.Staffs
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> GetAll();
        Staff GetById(int id);
        void Create(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
        IEnumerable<Staff> GetByCenter(int centerId);
    }
}
