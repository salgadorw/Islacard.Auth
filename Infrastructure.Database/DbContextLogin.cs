
using RWS.Authentication.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RWS.Authentication.Database
{
    public class DbContextLogin: DbContext
    {
        public DbContextLogin(DbContextOptions<DbContextLogin> dbContextOptions) : base(dbContextOptions)
        {
            
        }
        public DbSet<Login> Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseQueryTrackingBehavior( QueryTrackingBehavior.TrackAll);
                
    }
}