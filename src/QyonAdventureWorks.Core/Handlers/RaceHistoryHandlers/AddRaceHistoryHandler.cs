using MediatR;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Notifications;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Core.Handlers.RaceHistoryHandlers
{
    public class AddRaceHistoryHandler : IRequestHandler<AddRaceHistoryCommand, RaceHistory>
    {
        private readonly IRaceHistoryRepository raceHistoryRepository;
        private readonly INotificationService notificationService;

        public AddRaceHistoryHandler(IRaceHistoryRepository raceHistoryRepository, INotificationService notificationService)
        {
            this.raceHistoryRepository = raceHistoryRepository;
            this.notificationService = notificationService;
        }

        public async Task<RaceHistory> Handle(AddRaceHistoryCommand request, CancellationToken cancellationToken)
        {
            var raceHistory = request.ToEntity();
            
            var notifications = raceHistory.Validate();
            if(notifications.Any())
            {
                await notificationService.Notify(notifications);
                return null;
            }

            return await raceHistoryRepository.Add(raceHistory, cancellationToken);
        }
    }
}
