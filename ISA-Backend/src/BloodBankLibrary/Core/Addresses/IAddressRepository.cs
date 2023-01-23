using BloodBankLibrary.Core.Centers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Addresses
{
    public interface IAddressRepository
    {
        IEnumerable<CenterAddress> GetAll();
        CenterAddress GetById(int id);
        void Create(CenterAddress address);
        void Update(CenterAddress address);
        void Delete(CenterAddress address);
    }
}
