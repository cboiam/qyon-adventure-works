using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QyonAdventureWorks.Core.Commands.CircuitCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Queries;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CircuitsController : Controller
    {
        private readonly ICircuitQuery circuitQuery;
        private readonly IMediator mediator;

        public CircuitsController(ICircuitQuery circuitQuery, IMediator mediator, INotificationHandler<Notification> notifications)
            : base(notifications)
        {
            this.circuitQuery = circuitQuery;
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Circuit>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Circuit>>> Query([FromQuery] bool? used)
        {
            var result = await circuitQuery.Query(used);
            return ResponseGet(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Circuit), StatusCodes.Status200OK)]
        public async Task<ActionResult<Circuit>> Get(int id)
        {
            var result = await circuitQuery.Get(id);
            return ResponseGet(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Circuit), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Circuit>> Add(AddCircuitCommand driver)
        {
            var result = await mediator.Send(driver);

            return ResponsePost(result?.Id.ToString(), result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateCircuitCommand driver)
        {
            driver.Id = id;
            await mediator.Send(driver);

            return ResponsePut();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Circuit), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Circuit>> Delete(int id)
        {
            var result = await mediator.Send(new DeleteCircuitCommand(id));

            return ResponseDelete(result);
        }
    }
}
