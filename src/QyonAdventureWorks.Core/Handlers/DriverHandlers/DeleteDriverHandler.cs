using MediatR;
using QyonAdventureWorks.Core.Commands.DriverCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Handlers.DriverHandlers
{
    public class DeleteDriverHandler : IRequestHandler<DeleteDriverCommand, Driver>
    {
        private readonly IDriverRepository driverRepository;
        private readonly INotificationService notificationService;

        public DeleteDriverHandler(IDriverRepository driverRepository, INotificationService notificationService)
        {
            this.driverRepository = driverRepository;
            this.notificationService = notificationService;
        }

        public async Task<Driver> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var notifications = request.Validate();
            if (notifications.Any())
            {
                await notificationService.Notify(notifications);
                return null;
            }

            return await driverRepository.Delete(request.Id, cancellationToken);
        }
    }
}
