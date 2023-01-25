using BloodBankLibrary.Core.Donors;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly BloodBankDbContext _context;

        public UserRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user=>user.Email==email);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
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

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUserByDonor(Donor donor)
        {
            return _context.Users
                .Where(u => u.IdByType == donor.Id && u.UserType == Materials.Enums.UserType.DONOR)
                .FirstOrDefault<User>();
        }
    }
}
