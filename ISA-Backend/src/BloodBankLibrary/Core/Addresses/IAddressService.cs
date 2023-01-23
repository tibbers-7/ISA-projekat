using BloodBankLibrary.Core.Centers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Addresses
{
    public interface IAddressService
    {
        IEnumerable<CenterAddress> GetAll();
        CenterAddress GetById(int id);
        CenterAddress GetByCenter(int centerId);
        IEnumerable<string> GetCities();
        void Create(CenterAddress address);
        void Update(CenterAddress address);
        void Delete(CenterAddress address);
    }
}
