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
    public class DeleteCircuitHandler : IRequestHandler<DeleteCircuitCommand, Circuit>
    {
        private readonly ICircuitRepository circuitRepository;
        private readonly INotificationService notificationService;

        public DeleteCircuitHandler(ICircuitRepository circuitRepository, INotificationService notificationService)
        {
            this.circuitRepository = circuitRepository;
            this.notificationService = notificationService;
        }

        public async Task<Circuit> Handle(DeleteCircuitCommand request, CancellationToken cancellationToken)
        {
            var notifications = request.Validate();
            if (notifications.Any())
            {
                await notificationService.Notify(notifications);
                return null;
            }

            return await circuitRepository.Delete(request.Id, cancellationToken);
        }
    }
}
