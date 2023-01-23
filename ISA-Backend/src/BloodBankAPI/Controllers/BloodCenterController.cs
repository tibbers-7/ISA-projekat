using BloodBankLibrary.Core.Addresses;
using BloodBankLibrary.Core.Centers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodCenterController : ControllerBase
    {
        private readonly IBloodCenterService _bloodCenterService;
        private readonly IAddressService _addressService;

        public BloodCenterController(IBloodCenterService bloodCenterService, IAddressService addressService)
        {
            _bloodCenterService = bloodCenterService;
            _addressService = addressService;
        }

        // GET: api/bloodCenters
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodCenterService.GetAll());
        }

        // GET api/bloodCenters/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var bloodCenter = _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            return Ok(bloodCenter);
        }

        [HttpGet("cities")]
        public ActionResult GetCities()
        {
            var cities = _addressService.GetCities();
            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }


        // POST api/bloodCenters
        [HttpPost]
        public ActionResult Create(BloodCenter bloodCenter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bloodCenterService.Create(bloodCenter);
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
        public ActionResult Delete(int id)
        {
            var bloodCenter = _bloodCenterService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            _bloodCenterService.Delete(bloodCenter);
            return NoContent();
        }

        [HttpGet("{centerId}/donors")]
        public ActionResult GetDonorsForCenter(int centerId)
        {

            var donors = _bloodCenterService.GetDonorsByCenterId(centerId);
            if (donors == null)
            {
                return NotFound();
            }

            return Ok(donors);
        }


        [HttpGet("address/{id}")]
        public ActionResult GetAddressByCenter(int id) { 
        
        
            var address = _addressService.GetByCenter(id);
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
    }
}
