
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

        //ista fja za dodavanje available i schedule pa sam spojila
        [HttpPost("new")]
        public ActionResult NewAppointment(AppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointment = new Appointment(dto);
            //ako je false nije available
            if (!_appointmentService.CheckIfCenterAvailable(appointment.CenterId, appointment.StartDate, appointment.Duration))
            {
                return NotFound();
            }
            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        [HttpPost("schedule")]
        public ActionResult AddScheduled(AppointmentDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                Appointment _appointment =new Appointment(appointment);
                _appointment.Status = AppointmentStatus.SCHEDULED;
                _appointmentService.Update(_appointment);

                _appointmentService.GenerateAndSaveQR(_appointment);

                return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
            
            
        }


        [HttpPost("cancel")]
        public ActionResult Cancel(AppointmentDTO appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Appointment _appointment = new Appointment(appointment);
            _appointment.Status = AppointmentStatus.CANCELLED;
            _donorService.AddStrike(appointment.DonorId);
            _appointmentService.Update(_appointment);

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
            var appointments = _appointmentService.GetAvailableByCenter(centerId);
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

        [HttpGet("donor/available/{donorId}/{centerId}")]
        public ActionResult GetAvailableForDonor(int donorId,int centerId)
        {
            var appointments = _appointmentService.GetAvailableForDonor(donorId,centerId);
            if (appointments == null)
            {
                return NotFound();
            }
            return Ok(appointments);

        }

        [NonAction]
        private void sendEmail(Appointment appointment)
        {

            

        }



    }
}

