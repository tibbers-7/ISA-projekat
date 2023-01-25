using System.Collections.Generic;

namespace BloodBankLibrary.Core.Staffs
{
    public interface IStaffService
    {
        IEnumerable<Staff> GetAll();
        Staff GetById(int id);
        IEnumerable<Staff> GetByCenterId(int centerId);
        void Create(Staff staff);
        void Update(Staff staff);
        void Delete(Staff staff);
        


    }
}
