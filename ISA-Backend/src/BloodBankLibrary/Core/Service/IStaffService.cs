using BloodBankLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
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
