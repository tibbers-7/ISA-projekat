using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.Repository
{
    public class FormRepository : IFormRepository
    {
        private readonly BloodBankDbContext _context;
        public FormRepository(BloodBankDbContext context)
        {
            _context = context;
        }
        public void Create(Form form)
        {
            _context.Forms.Add(form);
            _context.SaveChanges();
        }


        public IEnumerable<Form> GetAll()
        {
            return _context.Forms.ToList();
        }

        public Form GetById(int id)
        {
            return _context.Forms.Find(id);
        }

        public void Update(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
