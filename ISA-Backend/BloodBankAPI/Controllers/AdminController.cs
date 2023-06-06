using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IUserService _adminService;

        public AdminController(IUserService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_adminService.GetAll());
        }

        // GET api/users/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var user = _adminService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

       

    }
}
