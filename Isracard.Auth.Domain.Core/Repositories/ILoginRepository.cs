using Isracard.Auth.Domain.Core.Models;

namespace Isracard.Auth.Domain.Core.Repositories
{
    public interface ILoginRepository
    {
        Task AddLoginValue(Login value);
        Task DeleteLoginValue(Guid id);
        Task<List<Login>> GetLoginValues();

        Task<Login?> GetLoginByUserName(string userName);
        
    }
}