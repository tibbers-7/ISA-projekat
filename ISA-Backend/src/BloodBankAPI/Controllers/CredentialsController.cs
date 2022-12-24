using BloodBankLibrary.Core.EmailSender;
using BloodBankLibrary.Core.Users;
using BloodBankLibrary.Core.Donors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace BloodBankAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CredentialsController : ControllerBase
	{


		private IUserService _userService;
		private JwtSecurityTokenHandler tokenHandler;
		private IEmailSendService _emailSendService;
		private IDonorService _donorService;
		public CredentialsController(IUserService userService, IEmailSendService emailSendService, IDonorService donorService)
		{

			_userService = userService;
			tokenHandler = new JwtSecurityTokenHandler();
			_emailSendService = emailSendService;
			_donorService = donorService;
		}

		[AllowAnonymous] //prevent the auth process to happen when calling
		[HttpPost("login")]
		public IActionResult Login(RegisterDTO regDTO)
		{
			User user = new User() { Email = regDTO.Email, Password = regDTO.Password };
			var _user = _userService.Authenticate(user);
			if (_user != null )
			{
				var token = Generate(_user);
				return Ok(tokenHandler.ReadToken(token));
			}

			return Unauthorized();
		}


		[AllowAnonymous]
		[HttpPost("register")]
		public ActionResult Register(RegisterDTO regDTO)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (_userService.GetByEmail(regDTO.Email) != null) return BadRequest("Exists");

			int idByRole = 0;
			string email = null;

			switch (regDTO.UserType)
            {
				case ("DONOR"):
                    {
						Donor donor = new Donor(regDTO);
						_donorService.Register(donor);
						donor = _donorService.GetByEmail(donor.Email);
						idByRole = donor.Id;
						email=donor.Email; // ako bude postojala provera da li je mejl dobro upisan
						break;
                    }
				case ("STAFF"):
                    {
						break;
                    }

				default: return (BadRequest("UserType"));
            }




			if (idByRole != 0 && email!=null)
			{
				User newUser = new User(regDTO, idByRole);
				_userService.Create(newUser);
				if (!SendActivationEmail(email)) return BadRequest("Email");
			}
			else return BadRequest("DatabaseError");

			

			return Ok();
		}


		private bool SendActivationEmail(string email)
		{

			var token = _userService.GenerateActivationToken(email);

			if (token != null)
			{
				_userService.SaveTokenToDatabase(email, token);

				var lnkHref = Url.Action("Activate", "Credentials", new { email = email, code = token }, "http");
				string subject = "HealthcareMD Activation Link";
				string body = "Your activation link: " + lnkHref;
				_emailSendService.SendEmail(new Message(new string[] { email, "tibbers707@gmail.com" }, subject, body));
				return true;
			}

			return false;
		}



		[AllowAnonymous]
		[HttpPost("send-activation")]
		public ActionResult SendActivationCode(RegisterDTO regDTO)
		{

			if (!ModelState.IsValid) return BadRequest();

			string email = regDTO.Email;
			var token = _userService.GenerateActivationToken(email);

			if (token != null)
			{
				_userService.SaveTokenToDatabase(email, token);

				var lnkHref = Url.Action("Activate", "Credentials", new { email = email, code = token }, "http");
				string subject = "HealthcareMD Activation Link";
				string body = "Your activation link: " + lnkHref;
				_emailSendService.SendEmail(new Message(new string[] { email, "tibbers707@gmail.com" }, subject, body));
				return Ok();
			}

			return NotFound();
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


		private string Generate(User user)
		{
			var token = _userService.GenerateFullToken(user);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
