using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Drawing;

namespace BloodBankAPI.Services.Users
{
    public interface IUserService
    {

        //IEnumerable<Staff> GetAll();
        //Staff GetById(int id);
        //IEnumerable<Staff> GetStaffByCenterId(int centerId);
        //void Create(Staff staff);
        //void Update(Staff staff);
        //void Delete(Staff staff);
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
