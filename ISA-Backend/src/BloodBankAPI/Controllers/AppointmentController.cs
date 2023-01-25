﻿
using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Appointments;
using BloodBankLibrary.Core.Donors;
using BloodBankLibrary.Core.Materials.Enums;
using BloodBankLibrary.Core.Materials.QRGenerator;
using BloodBankLibrary.Core.Centers;
using BloodBankLibrary.Core.Staffs;
using BloodBankLibrary.Core.EmailSender;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDonorService _donorService;

        public AppointmentController(IAppointmentService appointmentService, 
                                    IDonorService donorService)
        {
            _appointmentService = appointmentService;
            _donorService = donorService;
        }

       
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_appointmentService.GetAll());
        }

        
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }


        // Ovo je za zakazivanje postojecih od strane donora
        [HttpPost("schedule/predefined")]
        public ActionResult SchedulePredefined(AppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment = _appointmentService.GetById(dto.Id);
            appointment.DonorId = dto.DonorId;
            if (appointment.Status == AppointmentStatus.SCHEDULED)
            {
                //ukoliko je u medjuvremenu zakazan taj napravimo kopiju da bude cancelled
                appointment.Status = AppointmentStatus.CANCELLED;
                _appointmentService.Create(appointment);
                return BadRequest("Unavailable");
            }
            if (appointment.StaffId == 0) _appointmentService.AssignStaff(appointment);
            if (appointment == null) return BadRequest("Unavailable");
            appointment.Status = AppointmentStatus.SCHEDULED;
            _appointmentService.Update(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        // Ovo je za pravljenje available
        [HttpPost("staff/schedule")]
        public ActionResult ScheduleAppointmentStaff(AppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment = new Appointment(dto);
            if(!_appointmentService.IsStaffAvailable(appointment)) return BadRequest("Unavailable");
            if(!_appointmentService.CheckIfCenterAvailable(appointment.CenterId,appointment.StartDate, appointment.Duration)) return BadRequest("Unavailable");
            appointment.Status = AppointmentStatus.AVAILABLE;
            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        // U PrepareForDonorSchedule ubaciti i proveru da li postoji available/cancelled termin tada
        // Za cancelled ce ici opet provera da li je available staff i centar u to vreme 
        [HttpPost("donor/schedule")]
        public ActionResult AddScheduled(AppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            Appointment appointment = _appointmentService.PrepareForSchedule(dto);
            if (appointment == null) return BadRequest("Unavailable");
            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);


        }

        //otkazivanje pregleda odradjeno
        [HttpPost("cancel")]
        public ActionResult Cancel(AppointmentDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ukoliko je prekasno da otkaze izadje no content
            bool isSuccessful = _appointmentService.CancelAppt(appointment);
            if(!isSuccessful)  return NoContent();
            _donorService.AddStrike(appointment.DonorId);
            appointment.Status = AppointmentStatus.CANCELLED.ToString();
            return Ok(appointment);
        }


        // PUT api/appointments/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.Id)
            {
                return BadRequest();
            }

            try
            {
                _appointmentService.Update(appointment);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(appointment);
        }

        [HttpGet("centers/{dateTime}")]
        public ActionResult GetCentersForDateTime(string dateTime)
        {
            var bloodCenters = _appointmentService.GetCentersForDateTime(dateTime);
            if (bloodCenters == null)
            {
                return NotFound();
            }
            return Ok(bloodCenters);
        }

        [HttpGet("available/{centerId}")]
        public ActionResult GetAvailableForCenter(int centerId)
        {
            var appointments = _appointmentService.GetEligibleByCenter(centerId);
            if(appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [HttpGet("center/scheduled/{centerId}")]
        public ActionResult GetScheduledForCenter(int centerId)
        {
            var appointments = _appointmentService.GetScheduledByCenter(centerId);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }


        [HttpGet("center/future/{centerId}")]
        public ActionResult GetFutureForCenter(int centerId)
        {
            var appointments = _appointmentService.GetFutureByCenter(centerId);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [HttpGet("donor/scheduled/{id}")]
        public ActionResult GetScheduledForDonor(int id)
        {
            var appointments = _appointmentService.GetScheduledByDonor(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [HttpGet("donor/all/{id}")]
        public ActionResult GetAllForDonor(int id)
        {
            var appointments = _appointmentService.GetAllByDonor(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [HttpGet("donor/available/{donorId}/{centerId}")]
        public ActionResult GetAvailableForDonor(int donorId,int centerId)
        {
            var appointments = _appointmentService.GetEligibleForDonor(donorId,centerId);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }



    }
}

