using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Repository
{
    public class StaffRepository: IStaffRepository
    {
        private readonly BloodBankDbContext _context;

        public StaffRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Staff> GetAll()
        {
            return _context.Staff.ToList();
        }

        public Staff GetById(int id)
        {
            return _context.Staff.Find(id);
        }

        public void Create(Staff staff)
        {
            _context.Staff.Add(staff);
            _context.SaveChanges();
        }

        public void Update(Staff staff)
        {
            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Staff staff)
        {
            _context.Staff.Remove(staff);
            _context.SaveChanges();
        }
    }
}
