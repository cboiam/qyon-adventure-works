using MediatR;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Enums;

namespace QyonAdventureWorks.Core.Commands.DriverCommands
{
    public class AddDriverCommand : IRequest<Driver>
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public decimal BodyAvarageTemperature { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }

        public Driver ToEntity()
        {
            return new Driver(Name, Gender, BodyAvarageTemperature, Weight, Height);
        }
    }
}
