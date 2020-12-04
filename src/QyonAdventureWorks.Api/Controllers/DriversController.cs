using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QyonAdventureWorks.Core.Commands.DriverCommands;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Queries;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QyonAdventureWorks.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DriversController : Controller
    {
        private readonly IDriverQuery driverQuery;
        private readonly IMediator mediator;

        public DriversController(IDriverQuery driverQuery, IMediator mediator, INotificationHandler<Notification> notifications)
            : base(notifications)
        {
            this.driverQuery = driverQuery;
            this.mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<Driver>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Driver>>> Query(bool? veteran)
        {
            var result = await driverQuery.Query(veteran);
            return ResponseGet(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Driver), StatusCodes.Status200OK)]
        public async Task<ActionResult<Driver>> Get(int id)
        {
            var result = await driverQuery.Get(id);
            return ResponseGet(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Driver), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Driver>> Add(AddDriverCommand driver)
        {
            var result = await mediator.Send(driver);

            return ResponsePost(result?.Id.ToString(), result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, UpdateDriverCommand driver)
        {
            driver.Id = id;
            await mediator.Send(driver);

            return ResponsePut();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Driver), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Driver>> Delete(int id)
        {
            var result = await mediator.Send(new DeleteDriverCommand(id));

            return ResponseDelete(result);
        }
    }
}
