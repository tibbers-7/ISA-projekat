using BloodBankLibrary.Core.Model;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using UserModel = BloodBankLibrary.Core.Model.User;

namespace BloodBankLibrary.Core.Service
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        void Create(UserModel user);
        void Update(UserModel user);
        void Delete(UserModel user);
        UserModel GetByEmail(string email);
        bool Activate(string email, string token);
        bool SaveTokenToDatabase(string email, string token);

        public UserModel Authenticate(UserModel user);
        public SecurityToken GenerateFullToken(UserModel user);
        public string GenerateActivationToken(string email);



    }
}
