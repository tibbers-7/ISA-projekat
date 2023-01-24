using System;
using System.Collections.Generic;
using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Donors;

namespace BloodBankLibrary.Core.Appointments
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        IEnumerable<Appointment> GetScheduled();
        IEnumerable<AppointmentDTO> GetScheduledByDonor(int donorId);
        IEnumerable<Appointment> GetAvailable();
      
        IEnumerable<Appointment> GetByStaffId(int id);
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);
        IEnumerable<Appointment> GetAvailableByCenter(int centerId);
        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        void Create(Appointment appointment);
        void Update(Appointment appointment);

        bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration);
        Appointment CancelAppt(int apptId);
        object GetAvailableForDonor(int donorId, int centerId);
        Appointment GenerateAndSaveQR(Appointment appointment);
        object GetAllByDonor(int id);
        public IEnumerable<Donor> GetDonorsByCenterId(int centerId);
        Appointment AssignStaff(Appointment appointment);
        Appointment PrepareForSchedule(AppointmentDTO dto);
    }
}
