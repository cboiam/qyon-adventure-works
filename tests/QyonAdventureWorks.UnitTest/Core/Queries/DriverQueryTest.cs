using FluentAssertions;
using Moq;
using QyonAdventureWorks.Core.Entities;
using QyonAdventureWorks.Core.Interfaces.Repositories;
using QyonAdventureWorks.Core.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace QyonAdventureWorks.UnitTest.Core.Queries
{
    public class DriverQueryTest
    {
        private readonly Mock<IDriverRepository> driverRepository;
        private readonly DriverQuery instance;

        public DriverQueryTest()
        {
            driverRepository = new Mock<IDriverRepository>();
            instance = new DriverQuery(driverRepository.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAll_ShouldReturnDrivers(bool? veteran)
        {
            var drivers = new List<Driver>
            {
                new Driver(1),
                new Driver(2),
                new Driver(3)
            };

            driverRepository.Setup(x => x.Query(veteran))
                .ReturnsAsync(drivers);

            var result = await instance.Query(veteran);

            result.Should().BeEquivalentTo(drivers);
        }

        [Fact]
        public async Task Get_ShouldReturnDriver()
        {
            var driver = new Driver(1);

            driverRepository.Setup(x => x.Get(1))
                .ReturnsAsync(driver);

            var result = await instance.Get(1);

            result.Should().Be(driver);
        }
    }
}
