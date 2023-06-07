using BloodBankAPI.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /*
        // GET: api/users
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_userService.GetAll());
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

        // PUT api/users/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                _userService.Update(user);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(user);
        }

        // DELETE api/users/2
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.Delete(user);
            return NoContent();
        }
        */

    }
}
