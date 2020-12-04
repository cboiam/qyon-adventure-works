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
    public class AddCircuitHandlerTest
    {
        private readonly Mock<ICircuitRepository> circuitRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly AddCircuitHandler instance;

        public AddCircuitHandlerTest()
        {
            circuitRepository = new Mock<ICircuitRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new AddCircuitHandler(circuitRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenInvalidCircuit()
        {
            var result = await instance.Handle(new AddCircuitCommand(), new CancellationToken());

            result.Should().BeNull();
            circuitRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldReturnCircuit_WhenValidCircuit()
        {
            var command = new AddCircuitCommand
            {
                Description = "Description"
            };

            var circuit = new Circuit(default, command.Description);

            circuitRepository.Setup(x => x.Add(It.IsAny<Circuit>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(circuit);

            var result = await instance.Handle(command, new CancellationToken());

            result.Should().Be(circuit);
            notificationService.VerifyNoOtherCalls();
        }
    }
}
