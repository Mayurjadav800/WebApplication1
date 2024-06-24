using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebApplication1.Dto;
using WebApplication1.Model;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly EmployeeDbContext _dbContext;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IConfiguration configuration,EmployeeDbContext dbContext, IAuthenticationRepository authenticationRepository,ILogger<AuthenticationController>logger)
        {
            this.configuration = configuration;
            _dbContext = dbContext;
            _authenticationRepository = authenticationRepository;
            _logger = logger;
        }

        [HttpPost("Authentication")]
        public async Task<ActionResult> Auth([FromBody] LogginDto logginDto)
        {
            try
            {
                _logger.LogInformation("Create Api For Authentication");
                var token = await _authenticationRepository.CreateAuthentication(logginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error");
                return StatusCode(500, ex.Message);
            }
        }







        //[HttpPost("Authentication")]
        //[AllowAnonymous]
        //public object AuthenticateUser([FromBody] LogginDto logginDto)
        //{
        //    bool isValidUser = _authenticationRepository.AuthenticateUser(logginDto);
        //    if (isValidUser)
        //    {
        //        return GenerateToken(logginDto.Email);
        //    }
        //    else
        //    {
        //        return new Exception("Invalid user name and password");
        //    }
        //}
        //private string GenerateToken(string useremail)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,useremail),
        //    };
        //    var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"],
        //        this.configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}


    }
}
