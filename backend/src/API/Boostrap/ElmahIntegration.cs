using Core.Configuration;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Boostrap
{
    public static class ElmahIntegrationExtension
    {
        public static void AddElmahOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = configuration.Get<Settings>().ConnectionStrings.Elmah;
                options.Path = "/elmah";
            });
        }

    }
}
