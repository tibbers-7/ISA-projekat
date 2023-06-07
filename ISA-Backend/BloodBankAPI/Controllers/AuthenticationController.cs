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
		private IEmailSendService _emailSendService;
		public AuthenticationController(IAuthenticationService authService, IEmailSendService emailSendService)
		{

			_authService = authService;
			tokenHandler = new JwtSecurityTokenHandler();
			_emailSendService = emailSendService;
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
                return Ok("User registered successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
	/*
		
		/*
		private bool SendActivationEmail(string email,string temp)
		{

			var token = _userService.GenerateActivationToken(email);

			if (token != null)
			{
				_userService.SaveTokenToDatabase(email, token);

				var lnkHref = Url.Action("Activate", "Credentials", new { email = email, code = token }, "http");
				string subject = "BloodCenter Activation Link";
				string body = "Your activation link: " + lnkHref;
				if (temp != null)
                {
					body = body + "\nYour temporary password is: "+temp;
					body = body + "\nPlease change your password as soon as possible!";
                }
				_emailSendService.SendEmail(new Message(new string[] { email, "tibbers707@gmail.com" }, subject, body));
				return true;
			}

			return false;
		}

		[HttpGet("activate-account")]
		public ActionResult Activate()
		{
			string email = Request.Query["email"];
			string token = Request.Query["code"];


			if (ModelState.IsValid)
			{
				bool response = _userService.Activate(email, token);

				if (response)
				{
					return Redirect("http://localhost:4200/login");
				}
				else
				{
					return NotFound("Something went wrong!");
				}
			}
			return BadRequest();
		}
		*/
    }
}
