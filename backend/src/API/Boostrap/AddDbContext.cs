using Core.Configuration;
using Domain.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Boostrap
{
    public static class AddDbContextExtension
    {
        public static void AddDbContextService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AzureExampleContext>(b => b
                    .UseSqlServer(configuration.Get<Settings>().ConnectionStrings.AzureEapSeapolContext));

            serviceCollection.AddScoped<ExampleContext>(sp => sp.GetService<AzureExampleContext>());
        }
    }
}
