using System;
using System.Collections.Generic;
using BloodBankLibrary.Core.Centers;

namespace BloodBankLibrary.Core.Appointments
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        IEnumerable<Appointment> GetScheduled();
        IEnumerable<Appointment> GetScheduledByDonor(int donorId);
        IEnumerable<Appointment> GetAvailable();
      
        ICollection<Appointment> GetByStaffId(int id);
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);
        IEnumerable<Appointment> GetAvailableByCenter(int centerId);
        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        void Create(Appointment appointment);
        void Update(Appointment appointment);

        bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration);
        Appointment CancelAppt(int apptId);
        object GetAvailableForDonor(int donorId, int centerId);
    }
}
