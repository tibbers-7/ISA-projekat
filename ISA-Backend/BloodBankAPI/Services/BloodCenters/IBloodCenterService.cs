using System.Collections.Generic;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;

namespace BloodBankAPI.Services.BloodCenters
{
    public interface IBloodCenterService
    {
        Task<IEnumerable<CenterDTO>> GetAll();
        Task<BloodCenter> GetById(int id);
        Task Create(BloodCenter bloodCenter);
        void Update(BloodCenter bloodCenter);
        void Delete(BloodCenter bloodCenter);

        Task<IEnumerable<CenterDTO>> GetSearchResult(string content);

    }
}
