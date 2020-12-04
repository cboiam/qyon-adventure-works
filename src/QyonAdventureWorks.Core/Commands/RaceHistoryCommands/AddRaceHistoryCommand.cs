using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Commands.RaceHistoryCommands
{
    public class AddRaceHistoryCommand : IRequest<RaceHistory>
    {
        public int DriverId { get; set; }
        public int CircuitId { get; set; }
        public DateTime Date { get; set; }
        public decimal TimeSpent { get; set; }

        public RaceHistory ToEntity()
        {
            return new RaceHistory(DriverId, CircuitId, Date, TimeSpent);
        }
    }
}
