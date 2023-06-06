using BloodBankLibrary.Core.Staffs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {

        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_staffService.GetAll());
        }

       
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        
        [HttpGet("center/{centerId}")]
        public ActionResult GetByCenterId(int centerId)
        {
            var staff = _staffService.GetByCenterId(centerId);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

       
        [HttpPost]
        public ActionResult Create(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _staffService.Create(staff);
            return CreatedAtAction("GetById", new { id = staff.Id }, staff);
        }



        // PUT api/users/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.Id)
            {
                return BadRequest();
            }

            try
            {
                _staffService.Update(staff);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(staff);
        }

        // DELETE api/users/2
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff == null)
            {
                return NotFound();
            }

            _staffService.Delete(staff);
            return NoContent();
        }

    }
}
