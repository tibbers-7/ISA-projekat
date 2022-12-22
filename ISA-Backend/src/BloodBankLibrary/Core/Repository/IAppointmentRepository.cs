using BloodBankLibrary.Core.Model;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Repository
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
    }
}
