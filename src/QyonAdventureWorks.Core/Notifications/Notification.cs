using MediatR;

namespace QyonAdventureWorks.Core.Notifications
{
    public class Notification : INotification
    {
        public string Code { get; }
        public string Message { get; }

        public Notification(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
