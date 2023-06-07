using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BloodBankAPI.Controllers
{
    [AllowAnonymous]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{


		private IAuthenticationService _authService;
		private JwtSecurityTokenHandler tokenHandler;
		public AuthenticationController(IAuthenticationService authService)
		{

			_authService = authService;
			tokenHandler = new JwtSecurityTokenHandler();
		}


		[HttpPost("register/donor")]
		public async Task<IActionResult> RegisterDonor(DonorRegistrationDTO dto)
		{
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email "+ dto.Email+" already exists!");
                }

                await _authService.RegisterDonor(dto);
                await _authService.SendActivationLink(dto.Email);
                return Ok("User registered successfully, sending activation link!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("activate")]
        public async Task<ActionResult> Activate()
        {
            string email = Request.Query["email"];
            string token = Request.Query["token"];

            if (ModelState.IsValid)
            {
                try
                {
                    if ( await _authService.ActivateAccount(email, token))
                        return Redirect("http://localhost:4200/login");
                    else
                        return NotFound("Something went wrong!");
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return BadRequest();
        }


        [HttpPost("register/staff")]
        public async Task<IActionResult> RegisterStaff(StaffRegistrationDTO dto)
        {
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email " + dto.Email + " already exists!");
                }

                await _authService.RegisterStaff(dto);
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin(AdminRegistrationDTO dto)
        {
            try
            {
                if (await _authService.CheckIfEmailExistsAsync(dto.Email))
                {
                    return Conflict("Registration unsuccessful, user with email " + dto.Email + " already exists!");
                }

                await _authService.RegisterAdmin(dto);
                return Ok("User registered successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO dto)
		{
			try
			{
				if(await _authService.EmailMatchesPasswordAsync(dto))
				{
                    var tokenDTO = _authService.LogInUserAsync(dto);
					return Ok(tokenDTO);
				}
				else
				{
					return BadRequest("Log in unsuccessful");
				}
			}
			catch(Exception ex) {
                return BadRequest(ex.Message);
            }
		}



		/*
		[Authorize]
		[HttpPut("changePassword")]
		public IActionResult ChangePassword(string email,string newPass)
		{
			User user=_userService.GetByEmail(email);
			if (user == null) return BadRequest("NoUser");

			user.Password = newPass;
			

			if(!_userService.ChangePassword(user)) return BadRequest("ChangePassError");

            if (user.UserType == BloodBankLibrary.Core.Materials.Enums.UserType.STAFF)
            {
				Staff staff = _staffService.GetById(user.IdByType);
				staff.IsNew = false;
				_staffService.Update(staff);
			}
			
			return Ok();
		}
	

		
		*/
    }
}
