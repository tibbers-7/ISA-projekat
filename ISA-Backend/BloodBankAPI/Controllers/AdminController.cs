using BloodBankAPI.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        /*
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        // GET api/users/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

       */

    }
}
