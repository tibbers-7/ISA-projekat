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
        IEnumerable<Appointment> GetAvailable();
        IEnumerable<Appointment> GetAvailableByCenter(int centerId);
        IEnumerable<Appointment> GetScheduled();
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);



    }
}
