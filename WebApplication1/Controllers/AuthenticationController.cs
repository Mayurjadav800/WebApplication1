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
        private readonly EmployeeDbContext _dbContext;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(EmployeeDbContext dbContext, IAuthenticationRepository authenticationRepository)
        {
            _dbContext = dbContext;
            _authenticationRepository = authenticationRepository;
        }
        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<object> Auth([FromBody] LogginDto logginDto)
        {
            var user = await _authenticationRepository.CreateAuthentication(logginDto);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
    }
}
