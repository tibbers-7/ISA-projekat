using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly BloodBankDbContext _context;

        public AppointmentRepository(BloodBankDbContext context)
        {
            _context = context;
        }

        public void Create(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Delete(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments.ToList();
        }

        public Appointment GetById(int id)
        {
            return _context.Appointments.Find(id);
        }

        public void Update(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;

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
