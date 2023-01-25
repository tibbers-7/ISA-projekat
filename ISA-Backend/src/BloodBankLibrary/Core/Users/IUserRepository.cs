using BloodBankLibrary.Core.Donors;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);

        User GetUserByDonor(Donor donor);
        User GetByEmail(string email);
    }
}
