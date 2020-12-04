using FluentAssertions;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Enums;
using QyonAdventureWorks.Core.Notifications;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Entities
{
    public class DriverTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_ShouldContainNotification_WhenNameIsInvalid(string name)
        {
            var notifications = new Driver(name, Gender.Male, 0, 0, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.name", "Driver name cannot be empty"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenNameIsFilled()
        {
            var notifications = new Driver("Name", Gender.Male, 0, 0, 0)
                .Validate();

            notifications.Where(n => n.Code == "driver.name")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenBodyAvarageTemperatureIsNegative()
        {
            var notifications = new Driver(string.Empty, Gender.Male, -1, 0, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.bodyAvarageTemperature", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenBodyAvarageTemperatureIsZero()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 0, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.bodyAvarageTemperature", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenBodyAvarageTemperatureIsPositive()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 1, 0, 0)
                .Validate();

            notifications.Where(n => n.Code == "driver.bodyAvarageTemperature")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenWeightIsNegative()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, -1, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.weight", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenWeightIsZero()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 0, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.weight", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenWeightIsPositive()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 1, 0)
                .Validate();

            notifications.Where(n => n.Code == "driver.weight")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenHeightIsNegative()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 0, -1)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.height", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldContainNotification_WhenHeightIsZero()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 0, 0)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("driver.height", "This is not a valid value for a human being"));
        }

        [Fact]
        public void Validate_ShouldNotContainNotification_WhenHeightIsPositive()
        {
            var notifications = new Driver(string.Empty, Gender.Male, 0, 0, 1)
                .Validate();

            notifications.Where(n => n.Code == "driver.height")
                .Should().BeEmpty();
        }

        [Fact]
        public void AvarageTimeSpent_ShouldBeZero_WhenNullRaceHistories()
        {
            var driver = new Driver(default, default, default, default, default, default, null);
            driver.AvarageTimeSpent.Should().Be(0);
        }

        [Fact]
        public void AvarageTimeSpent_ShouldBeZero_WhenNoRaceHistories()
        {
            var driver = new Driver(default, default, default, default, default, default, new List<RaceHistory>());
            driver.AvarageTimeSpent.Should().Be(0);
        }

        [Fact]
        public void AvarageTimeSpent_ShouldBeOne()
        {
            var raceHistories = new List<RaceHistory>
            { 
                new RaceHistory(default, default, 0.5m),
                new RaceHistory(default, default, 1.5m),
                new RaceHistory(default, default, 1.25m),
                new RaceHistory(default, default, 0.75m),
            };

            var driver = new Driver(default, default, default, default, default, default, raceHistories);
            
            driver.AvarageTimeSpent.Should().Be(1);
        }
    }
}
