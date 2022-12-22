using System.Collections.Generic;
using UserModel = BloodBankLibrary.Core.Model.User;

namespace BloodBankLibrary.Core.Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        void Create(UserModel user);
        void Update(UserModel user);
        void Delete(UserModel user);
    }
}
