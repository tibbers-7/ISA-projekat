
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Model;
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

       
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _appointmentService.GetAll());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

   
        
        [HttpGet("{id}")]
        public async  Task<ActionResult> GetById(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetById(id);
                if (appointment == null)
                {
                    return NotFound();
                }

                return Ok(appointment);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        // Ovo je za pravljenje available, treba da se prosledi ovaj dto gde ce status biti available
        [HttpPost("staff/generate")]
        public async Task<ActionResult> GeneratePredefined(GeneratePredefinedAppointmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            if (! await _appointmentService.IsStaffAvailable(dto)) return BadRequest(" Staff is unavailable");
            if (! await _appointmentService.IsCenterAvailable(dto.CenterId, dto.StartDate, dto.Duration)) return BadRequest("Center is unavailable");
            Appointment appointment = await _appointmentService.GeneratePredefined(dto);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
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

        
        [HttpPost("donor/schedule")]
        public async Task<ActionResult> AddScheduled(AppointmentRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Appointment appointment = await _appointmentService.ScheduleIfAvailableAppointmentExists(dto);
            if(appointment == null) appointment =  await _appointmentService.GenerateDonorMadeAppointment(dto);
            if (appointment == null) BadRequest("Something went wrong");
           // _appointmentService
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);


        }

        /*
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

