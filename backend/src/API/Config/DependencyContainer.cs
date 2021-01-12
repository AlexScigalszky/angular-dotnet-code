using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Core.Repositories;
using Infrastructure.Logging;
using Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Core.Configuration;
using Core.Repositories.Base;
using Infrastructure.Repository.Base;
using Domain.Repositories;
using Domain.Repositories.Implementation;
using Domain.Data;
using Infrastructure.Data;

namespace API.Config
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(Startup));


            // Add Core Layer
            services.Configure<Settings>(configuration);


            // Add Infrastructure Layer
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<ExampleContext, AzureExampleContext>();
            // Add Repositories
            services.AddScoped<IProductRepository, ProductRepository>();


            // Add Aplication Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // TODO for example
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
