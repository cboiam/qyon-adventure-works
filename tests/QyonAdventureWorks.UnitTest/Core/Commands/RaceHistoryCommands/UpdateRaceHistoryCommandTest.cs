using FluentAssertions;
using QyonAdventureWorks.Core.Commands.RaceHistoryCommands;
using QyonAdventureWorks.Core.Notifications;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Commands.RaceHistoryCommands
{
    public class UpdateRaceHistoryCommandTest
    {
        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsNegative()
        {
            var notifications = new UpdateRaceHistoryCommand { Id = -1 }
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.id", "Race history id must be informed"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenIdIsZero()
        {
            var notifications = new UpdateRaceHistoryCommand()
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.id", "Race history id must be informed"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenIdIsValid()
        {
            var notifications = new UpdateRaceHistoryCommand { Id = 1 }
                .Validate();

            notifications.Where(n => n.Code == "raceHistory.id")
                .Should().BeEmpty();
        }
    }
}
