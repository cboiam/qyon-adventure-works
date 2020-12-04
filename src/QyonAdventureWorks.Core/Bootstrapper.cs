using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Queries;
using QyonAdventureWorks.Core.Notifications;
using QyonAdventureWorks.Core.Queries;
using System.Reflection;

namespace QyonAdventureWorks.Core
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            services.AddScoped<IDriverQuery, DriverQuery>();
            services.AddScoped<ICircuitQuery, CircuitQuery>();

            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
