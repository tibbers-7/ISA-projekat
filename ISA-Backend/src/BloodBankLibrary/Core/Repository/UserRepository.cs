using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UserModel = BloodBankLibrary.Core.Model.User;

namespace BloodBankLibrary.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BloodBankDbContext _context;

        public UserRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserModel GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Create(UserModel user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(UserModel user)
        {
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(UserModel user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
