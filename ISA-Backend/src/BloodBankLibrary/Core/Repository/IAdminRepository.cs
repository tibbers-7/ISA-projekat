using BloodBankLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Repository
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAll();
        Admin GetById(int id);
        void Create(Admin admin);
        void Update(Admin admin);
        void Delete(Admin admin);
    }
}
