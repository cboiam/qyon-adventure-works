using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Entities
{
    public class Circuit
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<RaceHistory> RaceHistories { get; private set; }

        public Circuit(int id)
        {
            Id = id;
        }

        public Circuit(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public Circuit(int id, string description, IEnumerable<RaceHistory> raceHistories)
            : this(id, description)
        {
            RaceHistories = raceHistories;
        }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (string.IsNullOrWhiteSpace(Description))
            {
                notifications.Add(new Notification("circuit.description", "Description should not be empty"));
            }

            return notifications;
        }
    }
}
