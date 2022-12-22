using BloodBankLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Service
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        IEnumerable<Appointment> GetScheduled();
        IEnumerable<Appointment> GetScheduledByDonor(int donorId);
        IEnumerable<Appointment> GetAvailable();
        IEnumerable<Appointment> GetCancelled();
        IEnumerable<Appointment> GetCompleted();
        ICollection<Appointment> GetByStaffId(int id);
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);
        IEnumerable<Appointment> GetAvailableByCenter(int centerId);
        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        void Create(Appointment appointment);
        void Update(Appointment appointment);

        bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration);
       
    }
}
