using FluentAssertions;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Notifications;
using System;
using System.Linq;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Entities
{
    public class RaceHistoryTest
    {
        [Fact]
        public void Validate_ShouldReturnNotifications_WhenDriverIdIsNegative()
        {
            var notifications = new RaceHistory(-1, default, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.driverId", "Driver id must be informed"));
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenDriverIdIsDefault()
        {
            var notifications = new RaceHistory(default, default, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.driverId", "Driver id must be informed"));
        }

        [Fact]
        public void Validate_ShouldNotReturnNotifications_WhenDriverIdIsValid()
        {
            var notifications = new RaceHistory(1, default, default, default)
                .Validate();

            notifications.Where(n => n.Code == "raceHistory.driverId")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenCircuitIdIsNegative()
        {
            var notifications = new RaceHistory(default, -1, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.circuitId", "Circuit id must be informed"));
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenCircuitIdIsDefault()
        {
            var notifications = new RaceHistory(default, default, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.circuitId", "Circuit id must be informed"));
        }

        [Fact]
        public void Validate_ShouldNotReturnNotifications_WhenCircuitIdIsValid()
        {
            var notifications = new RaceHistory(default, 1, default, default)
                .Validate();

            notifications.Where(n => n.Code == "raceHistory.circuitId")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenDateIsDefault()
        {
            var notifications = new RaceHistory(default, default, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.date", "Date is invalid"));
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenDateIsValid()
        {
            var notifications = new RaceHistory(default, default, DateTime.Now, default)
                .Validate();

            notifications.Where(n => n.Code == "raceHistory.date")
                .Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenTimeIsNegative()
        {
            var notifications = new RaceHistory(default, default, default, -1)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.timeSpent", "Time spent is invalid"));
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenTimeIsDefault()
        {
            var notifications = new RaceHistory(default, default, default, default)
                .Validate();

            notifications.Should().ContainEquivalentOf(new Notification("raceHistory.timeSpent", "Time spent is invalid"));
        }

        [Fact]
        public void Validate_ShouldReturnNotifications_WhenTimeIsValid()
        {
            var notifications = new RaceHistory(default, default, default, 1.53m)
                .Validate();

            notifications.Where(n => n.Code == "raceHistory.timeSpent")
                .Should().BeEmpty();
        }
    }
}
