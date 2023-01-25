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
        IEnumerable<Appointment> GetEligible();
      
        IEnumerable<Appointment> GetFutureByStaffId(int id);
        IEnumerable<Appointment> GetScheduledByCenter(int centerId);
        IEnumerable<Appointment> GetEligibleByCenter(int centerId);
        IEnumerable<AppointmentDTO> GetFutureByCenter(int centerId);
        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        void Create(Appointment appointment);
        void Update(Appointment appointment);

        bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration);
        bool CancelAppt(AppointmentDTO appointment);
        bool IsStaffAvailable(Appointment appointment);

        bool CanDonorCancel(int apptId);
         IEnumerable<AppointmentDTO >GetEligibleForDonor(int donorId, int centerId);
        Appointment GenerateAndSaveQR(Appointment appointment, string cancelReason);
        object GetAllByDonor(int id);
        IEnumerable<Donor> GetDonorsByCenterId(int centerId);
        Appointment AssignStaff(Appointment appointment);
        Appointment PrepareForSchedule(AppointmentDTO dto);
        void SendQRCancelled(Appointment appointment, int code);
    }
}
