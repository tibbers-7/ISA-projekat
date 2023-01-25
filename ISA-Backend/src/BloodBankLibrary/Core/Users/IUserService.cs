using BloodBankLibrary.Core.Donors;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Users
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        string Create(User user);
        void Update(User user);
        void Delete(User user);
        User GetByEmail(string email);
        bool Activate(string email, string token);
        bool SaveTokenToDatabase(string email, string token);

        public User Authenticate(User user);
        public SecurityToken GenerateFullToken(User user);
        public string GenerateActivationToken(string email);

        public Donor UpdateUserByDonor(Donor donor);
        bool ChangePassword(User user);

    }
}
