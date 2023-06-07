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


    }
}
