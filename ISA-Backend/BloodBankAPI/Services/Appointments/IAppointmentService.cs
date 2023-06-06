using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using System;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Appointments
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        bool IsStaffAvailable(Appointment appointment);
        bool CheckIfCenterAvailable(int centerId, DateTime dateTime, int duration);
        Appointment PrepareForSchedule(AppointmentDTO dto);
        void SendQRCancelled(Appointment appointment, int code);
        Appointment AssignStaff(Appointment appointment);
        bool CancelAppt(AppointmentDTO appointment);
        bool CanDonorCancel(int apptId);
        IEnumerable<BloodCenter> GetCentersForDateTime(string DateTime);
        IEnumerable<AppointmentDTO> GetFutureByCenter(int centerId);
        IEnumerable<AppointmentDTO> GetScheduledByDonor(int donorId);
        IEnumerable<AppointmentDTO> GetEligibleForDonor(int donorId, int centerId);
        IEnumerable<AppointmentDTO> GetHistoryForDonor(int donorId);
        object GetAllByDonor(int id);
        bool Overlaps(DateTime start1, DateTime end1, DateTime start2, DateTime end2);
        Appointment GenerateAndSaveQR(Appointment appointment, string cancelReason);
        IEnumerable<Appointment> GetEligibleByCenter(int centerId);

        IEnumerable<Appointment> GetScheduled();

        IEnumerable<Appointment> GetScheduledByCenter(int centerId);

        IEnumerable<AppointmentDTO> GetScheduledForStaff(int id);
        void CompleteAppt(AppointmentDTO appointment);
    }
}
