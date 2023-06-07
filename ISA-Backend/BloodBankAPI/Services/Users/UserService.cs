using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Donor>> GetAllDonors()
        {
            return await _unitOfWork.DonorRepository.GetAllAsync();
        }


    }
}
