using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Commands.DriverCommands
{
    public class DeleteDriverCommand : IRequest<Driver>
    {
        public int Id { get; set; }

        public DeleteDriverCommand(int id)
        {
            Id = id;
        }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (Id <= 0)
            {
                notifications.Add(new Notification("driver.id", "Driver id must be informed"));
            }

            return notifications;
        }
    }
}
