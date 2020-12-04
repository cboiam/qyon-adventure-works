using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Interfaces.Notifications
{
    public interface INotificationService
    {
        Task Notify(Notification notification);
        Task Notify(IEnumerable<Notification> notifications);
    }
}
