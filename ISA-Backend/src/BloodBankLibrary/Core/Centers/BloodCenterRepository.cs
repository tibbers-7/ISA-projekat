
using BloodBankLibrary.Core.Materials;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BloodBankLibrary.Core.Centers
{
    public class BloodCenterRepository : IBloodCenterRepository
    {
        private readonly BloodBankDbContext _context;

        public BloodCenterRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BloodCenter> GetAll()
        {
            List<BloodCenter> centers=new List<BloodCenter>();
            foreach (BloodCenter center in _context.BloodCenters)
            {
                center.Address = JsonSerializer.Deserialize<Address>(center.AddressJson);
                centers.Add(center);
            }
            return centers;
        }

        public BloodCenter GetById(int id)
        {
            return _context.BloodCenters.Find(id);
        }

        public void Create(BloodCenter bloodCenter)
        {
            _context.BloodCenters.Add(bloodCenter);
            _context.SaveChanges();
        }

        public void Update(BloodCenter bloodCenter)
        {
            _context.Entry(bloodCenter).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(BloodCenter bloodCenter)
        {
            _context.BloodCenters.Remove(bloodCenter);
            _context.SaveChanges();
        }
    }
}
