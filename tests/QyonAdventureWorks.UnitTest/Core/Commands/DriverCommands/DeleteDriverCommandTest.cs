using FluentAssertions;
using QyonAdventureWorks.Core.Commands.DriverCommands;
using QyonAdventureWorks.Core.Notifications;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Commands.DriverCommands
{
    public class DeleteCircuitCommandTest
    {
        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsNegative()
        {
            var notifications = new DeleteDriverCommand(-1)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.id", "Driver id must be informed"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsZero()
        {
            var notifications = new DeleteDriverCommand(0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.id", "Driver id must be informed"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenIdIsValid()
        {
            var notifications = new DeleteDriverCommand(1)
                .Validate();

            notifications.Where(n => n.Code == "driver.id")
                .Should().BeEmpty();
        }
    }
}
