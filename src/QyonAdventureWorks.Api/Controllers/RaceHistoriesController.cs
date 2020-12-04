using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RaceHistoriesController : Controller
    {
        private readonly IMediator mediator;

        public RaceHistoriesController(IMediator mediator, INotificationHandler<Notification> notifications)
            : base(notifications)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Driver), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RaceHistory>> Add(AddRaceHistoryCommand raceHistory)
        {
            var result = await mediator.Send(raceHistory);

            return ResponsePost(result?.Id.ToString(), result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateRaceHistoryCommand raceHistory)
        {
            raceHistory.Id = id;
            await mediator.Send(raceHistory);

            return ResponsePut();
        }
    }
}
