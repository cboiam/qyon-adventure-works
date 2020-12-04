using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Enums;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;

namespace QyonAdventureWorks.Core.Commands.DriverCommands
{
    public class UpdateDriverCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public decimal BodyAvarageTemperature { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }

        public Driver ToEntity()
        {
            return new Driver(Id, Name, Gender, BodyAvarageTemperature, Weight, Height);
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
