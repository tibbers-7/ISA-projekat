using BloodBankLibrary.Core.Model;
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
        ICollection<Appointment> GetByCenterId(int id);

        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
    }
}
