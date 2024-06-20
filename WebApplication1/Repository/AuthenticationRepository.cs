using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}