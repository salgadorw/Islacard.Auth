using RWS.Authentication.Application.Services.Dtos;

namespace RWS.Authentication.Application.Services
{
    public interface IAuthService
    {
        Task AddLoginValue(LoginDTO value);
        Task DeleteLoginValue(Guid Id);
        Task<List<LoginDTO>> GetLoginValues();

        Task<string> LogIn(LoginDTO login);
      
    }
}