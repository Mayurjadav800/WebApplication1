using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationRepository(EmployeeDbContext employeeDbContext, IConfiguration configuration)
        {
            _employeeDbContext = employeeDbContext;
            _configuration = configuration;
        }
        public async Task<string> CreateAuthentication(LogginDto logginDto)
        {
            var user = await _employeeDbContext.Loggins
           .FirstOrDefaultAsync(u => u.Email == logginDto.Email && u.Password == logginDto.Password);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");



            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,logginDto.Email),
               
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //     //new Claim(ClaimTypes.Password,user.Password),
            //    new Claim(ClaimTypes.Email, user.Email),
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    Issuer = _configuration["Jwt:Issuer"],
            //    Audience = _configuration["Jwt:Audience"],
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }




        //public bool AuthenticateUser(LogginDto logginDto)
        //{
        //    int rowcount = _employeeDbContext.Loggins.Where(i => i.Email == logginDto.Email && i.Password == logginDto.Password).Count();
        //    if (rowcount == 0)
        //        return false;
        //    else
        //    {
        //        return true;
        //    }
        //}

    }
}