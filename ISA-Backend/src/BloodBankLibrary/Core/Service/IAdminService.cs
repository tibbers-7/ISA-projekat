using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IAdminService
    {
        IEnumerable<Admin> GetAll();
        Admin GetById(int id);
        void Create(Admin admin);
        void Update(Admin admin);
        void Delete(Admin admin);

    }
}
