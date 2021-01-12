using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API.Boostrap
{
    public static class MigrationIntegrationExtension
    {
        public static void UseMigrations(this IApplicationBuilder applicationBuilder)
        {
            try
            {
                using var serviceScope = applicationBuilder
                    .ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope();

                serviceScope.ServiceProvider
                    .GetService<AzureExampleContext>()
                    .Database
                    .Migrate();

                Console.Write("Migration done.");
            }
            catch (Exception ex)
            {
                //EAP.Shared.Utils.ManageExceptionContext(new Exception("Failed to migrate or seed database" + ex));
                Console.Write($"Migration WRONG : {ex.Message}");
            }
        }
    }
}
