using FluentAssertions;
using Moq;
using QyonAdventureWorks.Core.Commands.DriverCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Enums;
using QyonAdventureWorks.Core.Handlers.DriverHandlers;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Handlers.DriverHandlers
{
    public class AddDriverHandlerTest
    {
        private readonly Mock<IDriverRepository> driverRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly AddDriverHandler instance;

        public AddDriverHandlerTest()
        {
            driverRepository = new Mock<IDriverRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new AddDriverHandler(driverRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenInvalidDriver()
        {
            var result = await instance.Handle(new AddDriverCommand(), new CancellationToken());

            result.Should().BeNull();
            driverRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnDriver_WhenValidDriver()
        {
            var command = new AddDriverCommand
            {
                Name = "Name",
                BodyAvarageTemperature = 36,
                Gender = Gender.Male,
                Height = 1.74m,
                Weight = 73
            };
            var driver = command.ToEntity();

            driverRepository.Setup(x => x.Add(It.IsAny<Driver>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(driver);

            var result = await instance.Handle(command, new CancellationToken());

            result.Should().Be(driver);
            notificationService.VerifyNoOtherCalls();
        }
    }
}
