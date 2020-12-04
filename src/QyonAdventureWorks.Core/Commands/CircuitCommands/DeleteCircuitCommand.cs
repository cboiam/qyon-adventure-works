using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Commands.CircuitCommands
{
    public class DeleteCircuitCommand : IRequest<Circuit>
    {
        public int Id { get; set; }

        public DeleteCircuitCommand(int id)
        {
            Id = id;
        }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (Id <= 0)
            {
                notifications.Add(new Notification("circuit.id", "Circuit id must be informed"));
            }

            return notifications;
        }
    }
}
