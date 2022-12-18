using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _staffRepository.GetAll();
        }

        public Staff GetById(int id)
        {
            return _staffRepository.GetById(id);
        }

        public void Create(Staff staff)
        {
            _staffRepository.Create(staff);
        }

        public void Update(Staff staff)
        {
            _staffRepository.Update(staff);
        }

        public void Delete(Staff staff)
        {
            _staffRepository.Delete(staff);
        }

        public IEnumerable<Staff> GetByCenterId(int centerId)
        {
            IEnumerable<Staff> allStaff = _staffRepository.GetAll();
            return allStaff.Where<Staff>(s => s.CenterId == centerId);
        }
    }
}
