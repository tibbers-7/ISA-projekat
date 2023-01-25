using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Appointments
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);

        IEnumerable<int> GetDonorsByCenter(int centerId);
        IEnumerable<Appointment> GetByDonor(int donorId);
        IEnumerable<Appointment> GetEligible();
        IEnumerable<Appointment> GetEligibleByCenter(int centerId);
        IEnumerable<Appointment> GetCancelledByDonorCenter(int donorId,int centerId);
        IEnumerable<Appointment> GetFutureByCenter(int centerId);
        IEnumerable<Appointment> GetScheduled();
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);

        IEnumerable<Appointment> GetFutureByStaff(int staffId);
        IEnumerable<Appointment> GetByDateAndStaff(int staffId, DateTime dateTime);




    }
}
