using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Infra.MySql.Interfaces;
using QyonAdventureWorks.Infra.MySql.Repositories;

namespace QyonAdventureWorks.Infra.MySql
{
    public static class Bootstraper
    {
        public static IServiceCollection RegisterMySqlDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QyonAdventureWorksContext>(options =>
            {
                var host = configuration["DBHOST"] ?? "localhost";
                var user = configuration["DBUSER"];
                var password = configuration["DBPASSWORD"];

                var connectionString = string.Format(configuration.GetConnectionString("MySqlConnection"), host, user, password);

                options.UseMySql(connectionString);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<ICircuitRepository, CircuitRepository>();
            services.AddScoped<IRaceHistoryRepository, RaceHistoryRepository>();

            return services;
        }

        public static IApplicationBuilder UseAutoMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<QyonAdventureWorksContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}
