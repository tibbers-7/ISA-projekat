using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    public class AdminService: IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public IEnumerable<Admin> GetAll()
        {
            return _adminRepository.GetAll();
        }

        public Admin GetById(int id)
        {
            return _adminRepository.GetById(id);
        }

        public void Create(Admin admin)
        {
            _adminRepository.Create(admin);
        }

        public void Update(Admin admin)
        {
            _adminRepository.Update(admin);
        }

        public void Delete(Admin admin)
        {
            _adminRepository.Delete(admin);
        }
    }
}
