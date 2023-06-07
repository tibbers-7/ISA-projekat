using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using System.Collections.Generic;
using System.Text.Json;

namespace BloodBankAPI.Services.BloodCenters
{
    public class BloodCenterService : IBloodCenterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BloodCenterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CenterDTO>> GetAll()
        {
            List<CenterDTO> result = new List<CenterDTO>();
            IEnumerable<BloodCenter> centers = await _unitOfWork.CenterRepository.GetAllAsync();
            _mapper.Map(centers, result, typeof(IEnumerable<BloodCenter>), typeof(IEnumerable<CenterDTO>));
            return result;
        }


        public async Task<BloodCenter> GetById(int id)
        {

            return await _unitOfWork.CenterRepository.GetByIdAsync(id);
        }

        public async Task Create(BloodCenter bloodCenter)
        {
           await _unitOfWork.CenterRepository.InsertAsync(bloodCenter);
        }

        public void Update(BloodCenter bloodCenter)
        {
            _unitOfWork.CenterRepository.Update(bloodCenter);
        }

        public void Delete(BloodCenter bloodCenter)
        {
            _unitOfWork.CenterRepository.Delete(bloodCenter);
        }

        public async Task<IEnumerable<CenterDTO>> GetSearchResult(string content)
        {
            List<CenterDTO> res = new List<CenterDTO>();

            foreach (CenterDTO center in await GetAll())
            {
                if (center.Name.ToLower().Contains(content.ToLower()) || center.stringAddress.ToLower().Contains(content.ToLower()))
                {
                    res.Add(center);
                }

            }

            return res;
        }
    }
}
