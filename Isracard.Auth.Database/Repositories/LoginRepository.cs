using Isracard.Auth.Domain.Core.Models;
using Isracard.Auth.Domain.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isracard.Auth.Database.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbContextLogin context;
        public LoginRepository(DbContextLogin dbContext)
        {
            this.context = dbContext;
        }

       

        public async Task AddLoginValue(Login value)
        {
            context.Logins.Add(value);
            await context.SaveChangesAsync();
        }

        public async Task DeleteLoginValue(Guid id)
        {
            var valueToRemove = context.Logins.FirstOrDefault();
            if (valueToRemove != null)
                context.Logins.Remove(valueToRemove);
           await context.SaveChangesAsync();

        }      

        public async Task<List<Login>> GetLoginValues()
        {            
            return await context.Logins.ToListAsync();
        }      

        public async Task<Login?> GetLoginByUserName(string userName)
        {
            return await context.Logins.Where(l=> l.UserName == userName).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
