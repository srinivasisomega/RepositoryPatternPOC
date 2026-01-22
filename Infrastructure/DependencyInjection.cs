using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // EF Core interceptor (must be singleton)
            services.AddSingleton<SqlCommandLoggingInterceptor>();

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var connectionString =
                    configuration.GetConnectionString("DefaultConnection");

                options.UseSqlServer(connectionString);

                // Interceptor
                options.AddInterceptors(
                    provider.GetRequiredService<SqlCommandLoggingInterceptor>());

                // Logging
                options.UseLoggerFactory(AppDbContext.EfLoggerFactory);
                options.LogTo(Console.WriteLine, LogLevel.Information);

                // POC only – remove in production
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();

                // Diagnostics
                options.ConfigureWarnings(w =>
                {
                    w.Log(RelationalEventId.ConnectionOpened);
                    w.Log(RelationalEventId.ConnectionClosed);
                    w.Log(RelationalEventId.CommandExecuted);
                });
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
