using MediatR;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator mediator;

        public NotificationService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Notify(Notification notification)
        {
            await mediator.Publish(notification);
        }

        public async Task Notify(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                await Notify(notification);
            }
        }
    }
}
