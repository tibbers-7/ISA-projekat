using BloodBankAPI.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public DonorController(IUserService userService)
        {
            _userService = userService;
        }

        /*
        // GET: api/Donor
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_donorService.GetAll());
        }

        // GET api/Donor/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var bloodCenter = _donorService.GetById(id);
            if (bloodCenter == null)
            {
                return NotFound();
            }

            return Ok(bloodCenter);
        }

        // PUT api/Donor/2
        [HttpPut("{id}")]
        public ActionResult Update(Donor donor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                donor=_userService.UpdateUserByDonor(donor);
                _donorService.Update(donor);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(donor);
        }

        */
    }
}
