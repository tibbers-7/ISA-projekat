using BloodBankLibrary.Core.Model;
using Microsoft.IdentityModel.Tokens;
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
        bool Activate(string email, string token);
        bool SaveTokenToDatabase(string email, string token);

        public User Authenticate(User user);
        public SecurityToken GenerateFullToken(User user);
        public string GenerateActivationToken(string email);



    }
}
