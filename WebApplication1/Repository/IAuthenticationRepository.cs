using WebApplication1.Dto;

namespace WebApplication1.Repository
{
    public interface IAuthenticationRepository
    {
        Task<string> CreateAuthentication(LogginDto logginDto);
    }
}
