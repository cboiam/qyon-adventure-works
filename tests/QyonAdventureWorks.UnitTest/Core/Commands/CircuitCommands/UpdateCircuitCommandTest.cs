using FluentAssertions;
using QyonAdventureWorks.Core.Commands.CircuitCommands;
using QyonAdventureWorks.Core.Notifications;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Commands.CircuitCommands
{
    public class UpdateCircuitCommandTest
    {
        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsNegative()
        {
            var notifications = new UpdateCircuitCommand { Id = -1 }
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("circuit.id", "Circuit id must be informed"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsZero()
        {
            var notifications = new UpdateCircuitCommand()
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("circuit.id", "Circuit id must be informed"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenIdIsValid()
        {
            var notifications = new UpdateCircuitCommand { Id = 1 }
                .Validate();

            notifications.Where(n => n.Code == "circuit.id")
                .Should().BeEmpty();
        }
    }
}
