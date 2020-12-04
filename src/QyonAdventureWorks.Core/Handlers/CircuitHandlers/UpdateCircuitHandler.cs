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
    public class UpdateCircuitHandler : IRequestHandler<UpdateCircuitCommand>
    {
        private readonly ICircuitRepository circuitRepository;
        private readonly INotificationService notificationService;

        public UpdateCircuitHandler(ICircuitRepository circuitRepository, INotificationService notificationService)
        {
            this.circuitRepository = circuitRepository;
            this.notificationService = notificationService;
        }

        public async Task<Unit> Handle(UpdateCircuitCommand request, CancellationToken cancellationToken)
        {
            var circuit = new Circuit(request.Id, request.Description);
            
            var notifications = request.Validate();
            notifications.AddRange(circuit.Validate());
            if (notifications.Any())
            {
                await notificationService.Notify(notifications);
                return Unit.Value;
            }

            await circuitRepository.Update(circuit, cancellationToken);

            return Unit.Value;
        }
    }
}
