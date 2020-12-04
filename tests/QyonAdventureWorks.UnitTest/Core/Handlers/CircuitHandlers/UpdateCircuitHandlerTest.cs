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
    public class UpdateCircuitHandlerTest
    {
        private readonly Mock<ICircuitRepository> circuitRepository;
        private readonly Mock<INotificationService> notificationService;
        private readonly UpdateCircuitHandler instance;

        public UpdateCircuitHandlerTest()
        {
            circuitRepository = new Mock<ICircuitRepository>();
            notificationService = new Mock<INotificationService>();

            instance = new UpdateCircuitHandler(circuitRepository.Object, notificationService.Object);
        }

        [Fact]
        public async Task Handle_ShouldNotCallRepository_WhenInvalidCircuit()
        {
            await instance.Handle(new UpdateCircuitCommand(), new CancellationToken());

            circuitRepository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Handle_ShouldNotCallNotification_WhenValidCircuit()
        {
            var command = new UpdateCircuitCommand
            {
                Id = 1,
                Description = "Description"
            };

            await instance.Handle(command, new CancellationToken());

            circuitRepository.Verify(x => x.Update(It.IsAny<Circuit>(), It.IsAny<CancellationToken>()));
            notificationService.VerifyNoOtherCalls();
        }
    }
}
