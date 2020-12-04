using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Commands.RaceHistoryCommands
{
    public class UpdateRaceHistoryCommand : IRequest
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public int CircuitId { get; set; }
        public DateTime Date { get; set; }
        public decimal TimeSpent { get; set; }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (Id <= 0)
            {
                notifications.Add(new Notification("raceHistory.id", "Race history id must be informed"));
            }

            return notifications;
        }

        public RaceHistory ToEntity()
        {
            return new RaceHistory(Id, DriverId, CircuitId, Date, TimeSpent);
        }
    }
}
