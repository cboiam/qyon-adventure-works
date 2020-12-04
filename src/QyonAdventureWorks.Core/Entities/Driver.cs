using QyonAdventureWorks.Core.Enums;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace QyonAdventureWorks.Core.Entities
{
    public class Driver
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public decimal BodyAvarageTemperature { get; private set; }
        public decimal Weight { get; private set; }
        public decimal Height { get; private set; }
        public IEnumerable<RaceHistory> RaceHistories { get; private set; }
        public decimal AvarageTimeSpent
        {
            get
            {
                if(RaceHistories == null || !RaceHistories.Any())
                {
                    return default;
                }
                return RaceHistories.Sum(r => r.TimeSpent) / RaceHistories.Count();
            }
        }

        public Driver(int id)
        {
            Id = id;
        }

        public Driver(string name, Gender gender, decimal bodyAvarageTemperature, decimal weight, decimal height)
        {
            Name = name;
            Gender = gender;
            BodyAvarageTemperature = bodyAvarageTemperature;
            Weight = weight;
            Height = height;
        }

        public Driver(int id, string name, Gender gender, decimal bodyAvarageTemperature, decimal weight, decimal height)
            : this(name, gender, bodyAvarageTemperature, weight, height)
        {
            Id = id;
        }

        public Driver(int id, string name, Gender gender, decimal bodyAvarageTemperature, decimal weight, decimal height, IEnumerable<RaceHistory> raceHistories)
            : this(id, name, gender, bodyAvarageTemperature, weight, height)
        {
            RaceHistories = raceHistories;
        }

        public List<Notification> Validate()
        {
            var notifications = new List<Notification>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                notifications.Add(new Notification("driver.name", "Driver name cannot be empty"));
            }

            if (BodyAvarageTemperature <= 0)
            {
                notifications.Add(new Notification("driver.bodyAvarageTemperature", "This is not a valid value for a human being"));
            }

            if (Weight <= 0)
            {
                notifications.Add(new Notification("driver.weight", "This is not a valid value for a human being"));
            }

            if (Height <= 0)
            {
                notifications.Add(new Notification("driver.height", "This is not a valid value for a human being"));
            }

            return notifications;
        }
    }
}
