using QyonAdventureWorks.Core.Notifications;
using System;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Entities
{
    public class RaceHistory
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public decimal TimeSpent { get; private set; }

        public int DriverId { get; private set; }
        public int CircuitId { get; private set; }
        
        public RaceHistory(int id, DateTime date, decimal timeSpent)
        {
            Id = id;
            Date = date;
            TimeSpent = timeSpent;
        }

        public RaceHistory(int driverId, int circuitId, DateTime date, decimal timeSpent)
            : this(default, date, timeSpent)
        {
            DriverId = driverId;
            CircuitId = circuitId;
        }

        public RaceHistory(int id, int driverId, int circuitId, DateTime date, decimal timeSpent)
            : this(id, date, timeSpent)
        {
            DriverId = driverId;
            CircuitId = circuitId;
        }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (DriverId <= 0)
            {
                notifications.Add(new Notification("raceHistory.driverId", "Driver id must be informed"));
            }

            if (CircuitId <= 0)
            {
                notifications.Add(new Notification("raceHistory.circuitId", "Circuit id must be informed"));
            }

            if (Date == default)
            {
                notifications.Add(new Notification("raceHistory.date", "Date is invalid"));
            }

            if (TimeSpent <= 0)
            {
                notifications.Add(new Notification("raceHistory.timeSpent", "Time spent is invalid"));
            }

            return notifications;
        }
    }
}
