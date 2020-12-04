using FluentAssertions;
using QyonAdventureWorks.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Handlers
{
    public class NotificationHandlerTest
    {
        private readonly NotificationHandler instance;

        public NotificationHandlerTest()
        {
            instance = new NotificationHandler();
        }

        [Fact]
        public async Task Handle_ShouldIncrementNotifications()
        {
            await instance.Handle(new Notification(string.Empty, string.Empty), new CancellationToken());

            instance.GetNotifications().Should().NotBeEmpty();
            instance.HasNotifications().Should().BeTrue();
        }

        [Fact]
        public void GetNotifications_ShouldBeEmpty_WhenInitialized()
        {
            instance.GetNotifications().Should().BeEmpty();
        }

        [Fact]
        public void HasNotifications_ShouldBeFalse_WhenInitialized()
        {
            instance.HasNotifications().Should().BeFalse();
        }
    }
}
