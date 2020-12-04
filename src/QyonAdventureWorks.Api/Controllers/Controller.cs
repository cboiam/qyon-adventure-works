using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace QyonAdventureWorks.Api.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class Controller : ControllerBase
    {
        private readonly NotificationHandler notifications;

        protected Controller(INotificationHandler<Notification> notifications)
        {
            this.notifications = (NotificationHandler)notifications;
        }

        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
            where T : class
        {
            if (result == null || !result.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
            where T : class
        {
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected ActionResult<T> ResponsePost<T>(string id, T result)
            where T : class
        {
            if (notifications.HasNotifications())
            {
                return GetErrorResponse();
            }

            if (result == null)
            {
                return NoContent();
            }

            return Created(GenerateAbsoluteUrl($"{Url.Action()}/{id}"), result);
        }

        protected ActionResult ResponsePut()
        {
            if (notifications.HasNotifications())
            {
                return GetErrorResponse();
            }

            return NoContent();
        }

        protected ActionResult<T> ResponseDelete<T>(T result)
            where T : class
        {
            if (notifications.HasNotifications())
            {
                return GetErrorResponse();
            }

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        private ActionResult GetErrorResponse()
        {
            var errors = notifications.GetNotifications()
                .GroupBy(n => n.Code)
                .Select(n => new KeyValuePair<string, string[]>(ToCamelCase(n.Key), n.Select(i => i.Message).ToArray()));

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(errors)));
        }

        private string ToCamelCase(string code)
        {
            return $"{char.ToLowerInvariant(code[0])}{code.Substring(1)}";
        }

        private string GenerateAbsoluteUrl(string path)
        {
            var uri = HttpContext.Request;
            var scheme = uri.Scheme;
            var host = uri.Host;
            return string.Format("{0}://{1}/{2}", scheme, host, path.TrimStart('/'));
        }
    }
}
