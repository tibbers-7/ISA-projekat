using BloodBankAPI.Model;
using BloodBankAPI.Services.Addresses;
using BloodBankAPI.Services.Appointments;
using BloodBankAPI.Services.BloodCenters;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodCenterController : ControllerBase
    {
        private readonly IBloodCenterService _bloodCenterService;
        private readonly IAddressService _addressService;
        private readonly IAppointmentService _appointmentService;
        public BloodCenterController(IBloodCenterService bloodCenterService, IAddressService addressService, IAppointmentService appointmentService)
        {
            _bloodCenterService = bloodCenterService;
            _addressService = addressService;
            _appointmentService=appointmentService;
        }

        // GET: api/bloodCenters
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _bloodCenterService.GetAll());
        }

        // GET api/bloodCenters/2
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var bloodCenter = await _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            return Ok(bloodCenter);
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetCities()
        {
            var cities = await _addressService.GetCitiesAsync();
            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }


        // POST api/bloodCenters
        [HttpPost]
        public async Task<ActionResult> Create(BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _bloodCenterService.Create(bloodCenter);
            return CreatedAtAction("GetById", new { id = bloodCenter.Id }, bloodCenter);
        }

        // PUT api/bloodCenters/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodCenter.Id)
            {
                return BadRequest();
            }

            try
            {
                _bloodCenterService.Update(bloodCenter);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodCenter);
        }

        // DELETE api/bloodCenters/2
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bloodCenter = await _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            _bloodCenterService.Delete(bloodCenter);
            return NoContent();
        }
/*
        [HttpGet("{centerId}/donors")]
        public ActionResult GetDonorsForCenter(int centerId)
        {

            var donors = _appointmentService.GetDonorsByCenterId(centerId);
            if (donors == null)
            {
                return NotFound();
            }

            return Ok(donors);
        }

*/
        [HttpGet("address/{id}")]
        public async Task<ActionResult> GetAddressByCenter(int id) { 
        
        
            var address = await _addressService.GetByCenterAsync(id);
            if(address== null)
            {
                return NotFound();
            }
            return Ok(address);
        
        }

        [HttpPut("address/{id}")]
        public ActionResult UpdateAddress(int id, CenterAddress address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
                _addressService.Update(address);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(address);
        }

        // POST api/bloodCenters
        [HttpPost("address")]
        public ActionResult CreateAddress(CenterAddress address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _addressService.Create(address);
            return CreatedAtAction("GetById", new { id = address.Id }, address);
        }

        [HttpGet("search/{content}")]
        public async Task<ActionResult> SearchResult(string content)
        {
            var result = await _bloodCenterService.GetSearchResult(content);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);

        }
    }
}
