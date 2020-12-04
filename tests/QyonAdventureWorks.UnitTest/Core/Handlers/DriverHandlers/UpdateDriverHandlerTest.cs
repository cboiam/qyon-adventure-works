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
    public class UpdateDriverHandlerTest
    {
        private readonly Mock<IDriverRepository> driverRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly UpdateDriverHandler instance;

        public UpdateDriverHandlerTest()
        {
            driverRepository = new Mock<IDriverRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new UpdateDriverHandler(driverRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldNotCallRepository_WhenInvalidDriver()
        {
            await instance.Handle(new UpdateDriverCommand(), new CancellationToken());

            driverRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldNotCallNotification_WhenInvalidDriver()
        {
            var command = new UpdateDriverCommand
            {
                Id = 1,
                Name = "Name",
                BodyAvarageTemperature = 36,
                Gender = Gender.Male,
                Height = 1.74m,
                Weight = 73
            };

            await instance.Handle(command, new CancellationToken());

            driverRepository.Verify(x => x.Update(It.IsAny<Driver>(), It.IsAny<CancellationToken>()));
            notificationService.VerifyNoOtherCalls();
        }
    }
}
