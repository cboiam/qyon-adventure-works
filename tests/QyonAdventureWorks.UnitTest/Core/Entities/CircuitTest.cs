using FluentAssertions;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Entities
{
    public class CircuitTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_ShouldContainNotification_WhenDescriptionIsInvalid(string description)
        {
            var notifications = new Circuit(default, description)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("circuit.description", "Description should not be empty"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenDescriptionIsFilled()
        {
            var notifications = new Circuit(default, "Description")
                .Validate();

            notifications.Where(n => n.Code == "circuit.description")
                .Should().BeEmpty();
        }
    }
}
