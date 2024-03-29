﻿using Jobsite.DAL.Data;
using Jobsite.DAL.Helpers;
using Jobsite.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jobsite.DAL.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly JobsiteContext _jobsiteContext;

        public UserService(IOptions<AppSettings> appSettings, JobsiteContext shopContext)
        {
            _appSettings = appSettings.Value;
            _jobsiteContext = shopContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _jobsiteContext.Users.SingleOrDefault(x => x.Email == username && x.Wachtwoord == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.Id.ToString()),
                    new Claim("Email", user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Wachtwoord = null;

            return user;
        }

    }
}
