using FluentAssertions;
using Moq;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Handlers.RaceHistoryHandlers;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Handlers.RaceHistoryHandlers
{
    public class AddRaceHistoryHandlerTest
    {
        private readonly Mock<IRaceHistoryRepository> raceHistoryRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly AddRaceHistoryHandler instance;

        public AddRaceHistoryHandlerTest()
        {
            raceHistoryRepository = new Mock<IRaceHistoryRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new AddRaceHistoryHandler(raceHistoryRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenInvalidRaceHistory()
        {
            var result = await instance.Handle(new AddRaceHistoryCommand(), new CancellationToken());

            result.Should().BeNull();
            raceHistoryRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnCircuit_WhenValidRaceHistory()
        {
            var command = new AddRaceHistoryCommand
            {
                DriverId = 1,
                CircuitId = 1,
                Date = DateTime.Now,
                TimeSpent = 1
            };

            var raceHistory = command.ToEntity();

            raceHistoryRepository.Setup(x => x.Add(It.IsAny<RaceHistory>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(raceHistory);

            var result = await instance.Handle(command, new CancellationToken());

            result.Should().Be(raceHistory);
            notificationService.VerifyNoOtherCalls();
        }
    }
}
