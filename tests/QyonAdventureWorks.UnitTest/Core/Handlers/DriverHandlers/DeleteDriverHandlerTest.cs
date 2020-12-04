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
    public class DeleteDriverHandlerTest
    {
        private readonly Mock<IDriverRepository> driverRepository;
        private readonly DeleteDriverHandler instance;

        private readonly Mock<INotificationService> notificationService;

        public DeleteDriverHandlerTest()
        {
            driverRepository = new Mock<IDriverRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new DeleteDriverHandler(driverRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenInvalidCommand()
        {
            var result = await instance.Handle(new DeleteDriverCommand(0), new CancellationToken());

            result.Should().Be(null);

            driverRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedDriver()
        {
            var driver = new Driver(1, "Name", Gender.Male, 36, 1.74m, 73);

            driverRepository.Setup(x => x.Delete(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(driver);

            var result = await instance.Handle(new DeleteDriverCommand(1), new CancellationToken());

            result.Should().Be(driver);

            notificationService.VerifyNoOtherCalls();
        }
    }
}
