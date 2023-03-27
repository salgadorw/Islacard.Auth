﻿using Isracard.Auth.Database;

namespace Isracard.Auth.Api
{
    internal static class DbInitialiserExtension
    {
        public static IApplicationBuilder InitialiseDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DbContextLogin>();
                DbInitialiser.Run(context);
            }
            return app;
        }
    }
}
