using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Addresses
{
    public class AddressRepository: IAddressRepository
    {
        private readonly BloodBankDbContext _context;

        public AddressRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CenterAddress> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public CenterAddress GetById(int id)
        {
            return _context.Addresses.Find(id);
        }


        public void Create(CenterAddress address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void Update(CenterAddress address)
        {
            _context.Entry(address).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(CenterAddress address)
        {
            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }
    }
}
