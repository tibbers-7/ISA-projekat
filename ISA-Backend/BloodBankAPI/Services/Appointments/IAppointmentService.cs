using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
using System;
using System.Collections.Generic;

namespace BloodBankAPI.Services.Appointments
{
    public interface IAppointmentService
    {

        Task<IEnumerable<AppointmentViewDTO>> GetAll();
        Task<AppointmentViewDTO> GetById(int id);
        Task Create(Appointment appointment);
        void Update(Appointment appointment);
        Task<IEnumerable<Donor>> GetDonorsByCenter(int centerId);
        Task<IEnumerable<AppointmentViewDTO>> GetScheduledByCenter(int centerId);
        Task<IEnumerable<AppointmentViewDTO>> GetAvailableByCenter(int centerId);
        Task<IEnumerable<AppointmentViewDTO>> GetScheduledByDonor(int donorId);
        Task<Appointment> GeneratePredefined(GeneratePredefinedAppointmentDTO dto);
        Task<bool> IsStaffAvailable(GeneratePredefinedAppointmentDTO dto);
        Task<bool> IsCenterAvailable(int centerId, string dateTime, int duration);
        Task<IEnumerable<AppointmentViewDTO>> GetHistoryForDonor(int donorId);
        Task<IEnumerable<CenterDTO>> GetCentersForDateTime(string DateTime);
        Task<Appointment> GenerateDonorMadeAppointment(AppointmentRequestDTO dto);
        Task<Appointment> ScheduleIfAvailableAppointmentExists(AppointmentRequestDTO dto);
        Appointment GenerateAndSaveQR(Appointment appointment, string cancelReason);
        /*     
        IEnumerable<AppointmentViewDTO> GetEligibleForDonor(int donorId, int centerId);
        void SendQRCancelled(Appointment appointment, int code);
        IEnumerable<Appointment> GetEligibleByCenter(int centerId);
        IEnumerable<AppointmentViewDTO> GetScheduledForStaff(int id);
        Task CompleteAppt(AppointmentViewDTO appointment);
        Task<bool> CancelAppt(AppointmentViewDTO appointment);

        */
    }
}
