using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Notifications
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        private readonly List<Notification> notifications;

        public NotificationHandler()
        {
            notifications = new List<Notification>();
        }

        public virtual List<Notification> GetNotifications()
        {
            return notifications;
        }

        public virtual bool HasNotifications()
        {
            return notifications.Any();
        }

        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            notifications.Add(notification);
            return Task.CompletedTask;
        }
    }
}
