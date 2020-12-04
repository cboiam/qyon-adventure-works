using FluentAssertions;
using Moq;
using QyonAdventureWorks.Core.Commands.CircuitCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Handlers.CircuitHandlers;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Handlers.CircuitHandlers
{
    public class DeleteCircuitHandlerTest
    {
        private readonly Mock<ICircuitRepository> circuitRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly DeleteCircuitHandler instance;

        public DeleteCircuitHandlerTest()
        {
            circuitRepository = new Mock<ICircuitRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new DeleteCircuitHandler(circuitRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenInvalidCommand()
        {
            var result = await instance.Handle(new DeleteCircuitCommand(0), new CancellationToken());

            result.Should().Be(null);

            circuitRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnDeletedCircuit()
        {
            var circuit = new Circuit(1, "Description");

            circuitRepository.Setup(x => x.Delete(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(circuit);

            var result = await instance.Handle(new DeleteCircuitCommand(1), new CancellationToken());

            result.Should().Be(circuit);

            notificationService.VerifyNoOtherCalls();
        }
    }
}
