using Autentication.API.Middleware;
using Autentication.Core.Interfaces;
using Autentication.Core.Notifications;
using Autentication.Core.Services;
using Autentication.Infrastructure.Persistence.Context;
using Autentication.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Autentication.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddDbContext<AutenticationContext>(
                x => x.UseSqlite(configuration["SqlLiteConnection:SqliteConnectionString"])
                .LogTo(Console.WriteLine, LogLevel.Information)
            );

            services.AddScoped<INotificator, Notificator>();

            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IJwtHandler, JwtHandler>();

            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}