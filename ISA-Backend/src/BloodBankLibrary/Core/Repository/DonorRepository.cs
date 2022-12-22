using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Repository
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodBankDbContext _context;

        public DonorRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Donor> GetAll()
        {
            return _context.Donors.ToList();
        }

        public Donor GetById(int id)
        {
            return _context.Donors.Find(id);
        }

        public void Create(Donor donor)
        {
            _context.Donors.Add(donor);
            _context.SaveChanges();
        }

        public void Update(Donor donor)
        {
            _context.Entry(donor).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Donor donor)
        {
            _context.Donors.Remove(donor);
            _context.SaveChanges();
        }
    }
}
