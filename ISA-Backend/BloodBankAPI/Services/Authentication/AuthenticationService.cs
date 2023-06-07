using AutoMapper;
using BloodBankAPI.Materials.DTOs;
using BloodBankAPI.Materials.EmailSender;
using BloodBankAPI.Materials.Enums;
using BloodBankAPI.Materials.PasswordHasher;
using BloodBankAPI.Model;
using BloodBankAPI.UnitOfWork;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloodBankAPI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IPasswordHasher _passwordHasher;
        private IEmailSendService _emailSendService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthenticationService(IUnitOfWork unitOfWork, IEmailSendService emailSendService, IPasswordHasher passwordHasher, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSendService = emailSendService;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<bool> CheckIfEmailExistsAsync(string email)
        {
            if (await GetUserByEmailAsync(email) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> EmailMatchesPasswordAsync(LoginDTO dto)
        {
            Account userByEmail = await GetUserByEmailAsync(dto.Email);
            if (userByEmail != null)
            {
                return _passwordHasher.VerifyHashedPassword(userByEmail.Password, dto.Password);
            }

            return false;

        }

        private async Task<Account> GetUserByEmailAsync(string email)
        {
            IEnumerable<Account> accounts =  await _unitOfWork.AccountRepository.GetByConditionAsync(u => u.Email == email);
            return accounts.FirstOrDefault();
        }

        public async Task<AccessTokenDTO> LogInUserAsync(LoginDTO dto)
        {
            Account account = await GetUserByEmailAsync(dto.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, account.UserType.ToString()),
                new Claim(ClaimTypes.PrimarySid, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email)
            };
            return GenerateToken(claims);
        }

        private AccessTokenDTO GenerateToken(IEnumerable<Claim> claims)
        {
          
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityTokenHandler().WriteToken(

                new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: credentials
                    ));

            return new AccessTokenDTO() { AccessToken = token };
        }


        public async Task RegisterDonor(DonorRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Donor donorData = _mapper.Map<Donor>(dto);
            donorData.UserType = UserType.DONOR;
            await _unitOfWork.DonorRepository.InsertAsync(donorData);
            await _unitOfWork.SaveAsync();
        }
       
        public async Task RegisterStaff(StaffRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Staff staffData = _mapper.Map<Staff>(dto);
            staffData.UserType = UserType.STAFF;
            await _unitOfWork.StaffRepository.InsertAsync(staffData);
            await _unitOfWork.SaveAsync();
        }

        public async Task RegisterAdmin(AdminRegistrationDTO dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            Admin adminData = _mapper.Map<Admin>(dto);
            adminData.UserType = UserType.ADMIN;
            await _unitOfWork.AdminRepository.InsertAsync(adminData);
            await _unitOfWork.SaveAsync();
        }

       

     
      



        /*
        public string Create(User user)
        {
            string tempPass = null;
            if (user.Password.Equals("") || user.Password == null)
            {
                user.Password = GeneratePassword(7);
                tempPass = user.Password;
            }
            string newPass = _passwordHasher.HashPassword(user.Password);
            user.Password = newPass;
            _userRepository.Create(user);
            return tempPass;
        }

        public bool ChangePassword(User user)
        {
            if (user == null) return false;
            user.Password = _passwordHasher.HashPassword(user.Password);

            Update(user);
            return true;

        }

        public bool Activate(string email, string token)
        {
            User user = GetByEmail(email);
            if (TokenValidity(user, token))
            {

                try
                {
                    user.Active = true;
                    user.Token = null;

                    Update(user);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }
        private bool TokenValidity(User user, string token)
        {
            if (!user.Token.Equals(token)) return false;
            SecurityToken _token = new JwtSecurityTokenHandler().ReadToken(token);

            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            DateTime validUntil = _token.ValidTo;
            DateTime localizedValid = TimeZoneInfo.ConvertTime(validUntil, tz);
            var unlocalized = DateTime.UtcNow;

            DateTime localizedNow = TimeZoneInfo.ConvertTime(unlocalized, tz);
            if (DateTime.Compare(localizedNow, localizedValid) > 0) return false;

            return true;
        }


        //Save activation token before sending an email
        public bool SaveTokenToDatabase(string email, string token)
        {
            User user = GetByEmail(email);
            if (user == null) return false; // ovo ne bi trebalo da se desi al ipak proveri

            user.Token = token;
            try
            {
                Update(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Check if the user is who he claims to be
        public User Authenticate(User user)
        {
            // UserConstraints -> baza
            User currentUser = _userRepository.GetByEmail(user.Email);
            if (currentUser == null || !currentUser.Active) return null;
            if (_passwordHasher.VerifyHashedPassword(currentUser.Password, user.Password)) return currentUser;

            return null;
        }

        public SecurityToken GenerateFullToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //HmacSha256 - hashing algorithm


            var claims = new[] //a way to store the data so that you don't have to always access the db
			{ //these are set-in-stone claims (NameIdentifier, Email, GivenName)
				new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.PrimaryGroupSid, user.IdByType.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return token;
        }

        //A separate method, because activation doesn't require claims
        // It only needs an expiration time
        public string GenerateActivationToken(string email)
        {
            User user = GetByEmail(email);
            if (user == null) return null;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var unlocalized = DateTime.UtcNow.AddMinutes(15);
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
            DateTime localizedTime = TimeZoneInfo.ConvertTime(unlocalized, tz);

            var claims = new[]
             {
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: localizedTime,
                signingCredentials: credentials);



            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GeneratePassword(int length)
        {
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                if (char.IsLetterOrDigit(c)) password.Append(c);

            }

            return password.ToString();
        }

        public Donor UpdateUserByDonor(Donor donor)
        {
            User user = _userRepository.GetUserByDonor(donor);
            user.Password = _passwordHasher.HashPassword(donor.Password);
            user.Name = donor.Name;
            user.Surname = donor.Surname;
            _userRepository.Update(user);

            donor.Password = null;
            donor.Address = new PrivateAddress(donor.AddressString);
            return donor;
        }

        public void Register(DonorRegistrationDTO dto)
        {
            throw new NotImplementedException();
        }
        */
    }
}
