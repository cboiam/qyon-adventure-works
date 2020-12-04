using MediatR;
using QyonAdventureWorks.Core.Commands.CircuitCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Handlers.CircuitHandlers
{
    public class AddCircuitHandler : IRequestHandler<AddCircuitCommand, Circuit>
    {
        private readonly ICircuitRepository circuitRepository;
        private readonly INotificationService notificationService;

        public AddCircuitHandler(ICircuitRepository circuitRepository, INotificationService notificationService)
        {
            this.circuitRepository = circuitRepository;
            this.notificationService = notificationService;
        }

        public async Task<Circuit> Handle(AddCircuitCommand request, CancellationToken cancellationToken)
        {
            var circuit = new Circuit(default, request.Description);
            
            var notifications = circuit.Validate();
            if(notifications.Any())
            {
                await notificationService.Notify(notifications);
                return null;
            }

            return await circuitRepository.Add(circuit, cancellationToken);
        }
    }
}
