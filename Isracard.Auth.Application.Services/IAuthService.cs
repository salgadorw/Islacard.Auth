using Isracard.Auth.Application.Services.Dtos;

namespace Isracard.Auth.Application.Services
{
    public interface IAuthService
    {
        Task AddLoginValue(LoginDTO value);
        Task DeleteLoginValue(Guid Id);
        Task<List<LoginDTO>> GetLoginValues();

        Task<string> LogIn(LoginDTO login);
      
    }
}