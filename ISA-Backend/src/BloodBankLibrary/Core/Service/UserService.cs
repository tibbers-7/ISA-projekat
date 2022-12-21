﻿using BloodBankLibrary.Core.Model;
using BloodBankLibrary.Core.Repository;
using HospitalLibrary.Core.EmailSender;
using HospitalLibrary.Core.PasswordHasher;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BloodBankLibrary.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;
        private IPasswordHasher _passwordHasher;
        private IEmailSendService _emailSendService;
        public UserService(IConfiguration config, IUserRepository userRepository, IEmailSendService emailSendService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _config = config;
            _emailSendService = emailSendService;
            _passwordHasher = passwordHasher;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetByEmail(string email)
        {
            foreach(User u in _userRepository.GetAll())
                if (u.Email == email)
                    return u;
             return null;
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
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
            var users = _userRepository.GetAll();
            var currentUser = users.FirstOrDefault(o => o.Email.ToLower() ==
                user.Email.ToLower());
            if (currentUser == null) return null;
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
                new Claim(ClaimTypes.PrimaryGroupSid, user.IdByRole.ToString()),
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

    }
}
