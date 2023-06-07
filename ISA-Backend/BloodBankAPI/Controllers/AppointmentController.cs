
using BloodBankAPI.Services.Appointments;
using Microsoft.AspNetCore.Mvc;
namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

       /*
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

        // Ovo je za pravljenje available
        [HttpPost("staff/schedule")]
        public ActionResult ScheduleAppointmentStaff(AppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Appointment appointment = new Appointment(dto);
            if (!_appointmentService.IsStaffAvailable(appointment)) return BadRequest("Unavailable");
            if (!_appointmentService.CheckIfCenterAvailable(appointment.CenterId, appointment.StartDate, appointment.Duration)) return BadRequest("Unavailable");
            appointment.Status = AppointmentStatus.AVAILABLE;
            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

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
                _appointmentService.SendQRCancelled(appointment, 1);
                return BadRequest("Unavailable");
            }
            if (appointment.StaffId == 0) _appointmentService.AssignStaff(appointment);
            if (appointment == null)
            {
                _appointmentService.SendQRCancelled(appointment, 2);
                return BadRequest("Unavailable");
            }
            appointment.Status = AppointmentStatus.SCHEDULED;

            _appointmentService.Update(appointment);
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
            return Ok(_appointmentService.GetScheduledByDonor(appointment.DonorId));
        }

        [HttpPost("complete")]
        public ActionResult Complete(AppointmentDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             _appointmentService.CompleteAppt(appointment);

            return Ok(_appointmentService.GetScheduledByDonor(appointment.DonorId));
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

        [HttpGet("donor/eligible/{donorId}/{centerId}")]
        public ActionResult GetAvailableForDonor(int donorId, int centerId)
        {
            var appointments = _appointmentService.GetEligibleForDonor(donorId, centerId);
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

        [HttpGet("donor/history/{id}")]
        public ActionResult GetHistoryForDonor(int id)
        {
            var appointments = _appointmentService.GetHistoryForDonor(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }


        [HttpGet("staff/scheduled/{id}")]
        public ActionResult GetScheduledForStaff(int id)
        {
            var appointments = _appointmentService.GetScheduledForStaff(id);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }


        */

    }
}

