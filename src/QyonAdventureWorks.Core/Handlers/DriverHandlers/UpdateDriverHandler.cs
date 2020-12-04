using MediatR;
using QyonAdventureWorks.Core.Commands.DriverCommands;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Handlers.DriverHandlers
{
    public class UpdateDriverHandler : IRequestHandler<UpdateDriverCommand>
    {
        private readonly IDriverRepository driverRepository;
        private readonly INotificationService notificationService;

        public UpdateDriverHandler(IDriverRepository driverRepository, INotificationService notificationService)
        {
            this.driverRepository = driverRepository;
            this.notificationService = notificationService;
        }

        public async Task<Unit> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var driver = request.ToEntity();
            
            var notifications = request.Validate();
            notifications.AddRange(driver.Validate());
            if (notifications.Any())
            {
                await notificationService.Notify(notifications);
                return Unit.Value;
            }

            await driverRepository.Update(driver, cancellationToken);

            return Unit.Value;
        }
    }
}
