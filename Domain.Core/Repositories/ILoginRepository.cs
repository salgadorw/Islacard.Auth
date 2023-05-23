using RWS.Authentication.Domain.Core.Models;

namespace RWS.Authentication.Domain.Core.Repositories
{
    public interface ILoginRepository
    {
        Task AddLoginValue(Login value);
        Task DeleteLoginValue(Guid id);
        Task<List<Login>> GetLoginValues();

        Task<Login?> GetLoginByUserName(string userName);
        
    }
}