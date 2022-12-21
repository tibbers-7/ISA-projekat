using BloodBankLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Service
{
    public interface IDonorService
    {
        IEnumerable<Donor> GetAll();
        Donor GetById(int id);
        void Register(Donor donor);
        void Update(Donor donor);
        void Delete(Donor donor);
        Donor GetByEmail(string email);
        
    }
}
