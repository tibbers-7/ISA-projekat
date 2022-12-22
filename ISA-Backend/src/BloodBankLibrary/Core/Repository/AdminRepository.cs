using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Repository
{
    public class AdminRepository : IAdminRepository
    {
        
            private readonly BloodBankDbContext _context;

            public AdminRepository(BloodBankDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Admin> GetAll()
            {
                return _context.Admins.ToList();
            }

            public Admin GetById(int id)
            {
                return _context.Admins.Find(id);
            }

            public void Create(Admin admin)
            {
                _context.Admins.Add(admin);
                _context.SaveChanges();
            }

            public void Update(Admin admin)
            {
                _context.Entry(admin).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            public void Delete(Admin admin)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        
    }
}
