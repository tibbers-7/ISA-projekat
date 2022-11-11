using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
        User GetByEmail(string email);
    }
}
