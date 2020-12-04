using MediatR;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Handlers.RaceHistoryHandlers
{
    public class UpdateRaceHistoryHandler : IRequestHandler<UpdateRaceHistoryCommand>
    {
        private readonly IRaceHistoryRepository raceHistoryRepository;
        private readonly INotificationService notificationService;

        public UpdateRaceHistoryHandler(IRaceHistoryRepository raceHistoryRepository, INotificationService notificationService)
        {
            this.raceHistoryRepository = raceHistoryRepository;
            this.notificationService = notificationService;
        }

        public async Task<Unit> Handle(UpdateRaceHistoryCommand request, CancellationToken cancellationToken)
        {
            var raceHistory = request.ToEntity();
            
            var notifications = request.Validate();
            notifications.AddRange(raceHistory.Validate());
            if (notifications.Any())
            {
                await notificationService.Notify(notifications);
                return Unit.Value;
            }

            await raceHistoryRepository.Update(raceHistory, cancellationToken);

            return Unit.Value;
        }
    }
}
