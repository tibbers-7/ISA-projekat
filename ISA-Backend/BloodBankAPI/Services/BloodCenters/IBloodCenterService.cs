using System.Collections.Generic;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;

namespace BloodBankAPI.Services.BloodCenters
{
    public interface IBloodCenterService
    {
        IEnumerable<CenterDTO> GetAll();
        BloodCenter GetById(int id);
        void Create(BloodCenter bloodCenter);
        void Update(BloodCenter bloodCenter);
        void Delete(BloodCenter bloodCenter);

        IEnumerable<CenterDTO> GetSearchResult(string content);

    }
}
