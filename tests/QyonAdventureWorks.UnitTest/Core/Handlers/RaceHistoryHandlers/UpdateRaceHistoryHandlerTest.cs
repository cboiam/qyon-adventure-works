using Moq;
using QyonAdventureWorks.Core.Commands.CircuitCommands;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Handlers.CircuitHandlers;
using QyonAdventureWorks.Core.Handlers.RaceHistoryHandlers;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Handlers.RaceHistoryHandlers
{
    public class UpdateRaceHistoryHandlerTest
    {
        private readonly Mock<IRaceHistoryRepository> raceHistoryRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly UpdateRaceHistoryHandler instance;

        public UpdateRaceHistoryHandlerTest()
        {
            raceHistoryRepository = new Mock<IRaceHistoryRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new UpdateRaceHistoryHandler(raceHistoryRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldNotCallRepository_WhenInvalidCircuit()
        {
            await instance.Handle(new UpdateRaceHistoryCommand(), new CancellationToken());

            raceHistoryRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldNotCallNotification_WhenValidCircuit()
        {
            var command = new UpdateRaceHistoryCommand
            {
                Id = 1,
                DriverId = 1,
                CircuitId = 1,
                Date = DateTime.Now,
                TimeSpent = 1
            };

            await instance.Handle(command, new CancellationToken());

            raceHistoryRepository.Verify(x => x.Update(It.IsAny<RaceHistory>(), It.IsAny<CancellationToken>()));
            notificationService.VerifyNoOtherCalls();
        }
    }
}
