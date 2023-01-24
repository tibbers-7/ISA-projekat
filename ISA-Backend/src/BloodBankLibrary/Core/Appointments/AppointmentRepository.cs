using BloodBankLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Appointments
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

        public IEnumerable<Appointment> GetByDonor(int donorId)
        {
            return _context.Appointments.Where(appt=>appt.DonorId==donorId);
        }

        public IEnumerable<Appointment> GetByStaff(int staffId)
        {
            return _context.Appointments.Where(appt => appt.StaffId == staffId);
        }

        public IEnumerable<Appointment> GetEligible()
        {
            return _context.Appointments.Where(appt=>appt.Status==Materials.Enums.AppointmentStatus.AVAILABLE || appt.Status==Materials.Enums.AppointmentStatus.CANCELLED);
        }

        public IEnumerable<Appointment> GetEligibleByCenter(int centerId)
        {
            return _context.Appointments.Where(appt =>appt.CenterId==centerId && (appt.Status == Materials.Enums.AppointmentStatus.AVAILABLE || appt.Status == Materials.Enums.AppointmentStatus.CANCELLED));
        }

        public IEnumerable<Appointment> GetScheduled()
        {
            return _context.Appointments.Where(appt => appt.Status == Materials.Enums.AppointmentStatus.SCHEDULED);
        }

        public IEnumerable<Appointment> GetScheduledByCenter(int centerId)
        {
            return _context.Appointments.Where(appt =>appt.CenterId==centerId && appt.Status == Materials.Enums.AppointmentStatus.SCHEDULED);
        }

        public IEnumerable<Appointment> GetScheduledByDonor(int donorId)
        {
            return _context.Appointments.Where(appt => appt.DonorId == donorId && appt.Status == Materials.Enums.AppointmentStatus.SCHEDULED);
        }
        public IEnumerable<int> GetDonorsByCenter(int centerId)
        {
            var appointments = 
                _context.Appointments.Where(appt => appt.CenterId == centerId && appt.Status==Materials.Enums.AppointmentStatus.COMPLETED)
                                                    .Select(appt=>appt.DonorId)
                                                    .Distinct();
            return appointments;
        }

        public IEnumerable<Appointment> GetByDateAndStaff(int staffId, DateTime dateTime)
        {
            return _context.Appointments.Where(appt => appt.StaffId == staffId && appt.StartDate.Date == dateTime.Date);
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
