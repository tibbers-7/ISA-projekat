using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Service;
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


        [HttpPost("available/add")]
        public ActionResult AddAvailable(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //ako je false nije available
            if (!_appointmentService.CheckIfCenterAvailable(appointment.CenterId, appointment.StartDate, appointment.Duration))
            {
                return NotFound();
            }
            appointment.Status = BloodBankLibrary.Core.Model.Enums.AppointmentStatus.AVAILABLE;
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        [HttpPost("schedule")]
        public ActionResult AddScheduled(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            appointment.Status = BloodBankLibrary.Core.Model.Enums.AppointmentStatus.SCHEDULED;
            _appointmentService.Update(appointment);

            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
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

        [HttpGet("available/center/{centerId}")]
        public ActionResult GetAvailableForCenter(int centerId)
        {
            var appointments = _appointmentService.GetAvailableByCenter(centerId);
            if(appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [HttpGet("donor/scheduled/{id}")]
        public ActionResult GetScheduledForDonor(int donorId)
        {
            var appointments = _appointmentService.GetScheduledByDonor(donorId);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }




    }
}

