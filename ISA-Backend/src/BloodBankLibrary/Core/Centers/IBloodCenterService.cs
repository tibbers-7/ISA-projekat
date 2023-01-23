using System.Collections.Generic;
using BloodBankLibrary.Core.Donors;

namespace BloodBankLibrary.Core.Centers
{
    public interface IBloodCenterService
    {
        IEnumerable<CenterDTO> GetAll();
        BloodCenter GetById(int id);
        void Create(BloodCenter bloodCenter);
        void Update(BloodCenter bloodCenter);
        void Delete(BloodCenter bloodCenter);
        IEnumerable<Donor> GetDonorsByCenterId(int centerId);
       
    }
}
