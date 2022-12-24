using System.Collections.Generic;

namespace BloodBankLibrary.Core.Admins
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
