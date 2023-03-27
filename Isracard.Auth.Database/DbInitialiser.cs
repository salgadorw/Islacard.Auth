using Isracard.Auth.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isracard.Auth.Database
{
    public static class DbInitialiser 
    {

        public static void Run(DbContextLogin context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Logins.Add(new Login() { Id = Guid.NewGuid(), UserName = "admin@admin.com", Password = "admin" });
            context.Logins.Add(new Login() { Id = Guid.NewGuid(), UserName = "other@admin.com", Password = "other" });
            context.SaveChanges();
        }

    }
}
