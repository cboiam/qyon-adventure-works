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
    public class CircuitQueryTest
    {
        private readonly Mock<ICircuitRepository> circuitRepository;
        private readonly CircuitQuery instance;

        public CircuitQueryTest()
        {
            circuitRepository = new Mock<ICircuitRepository>();
            instance = new CircuitQuery(circuitRepository.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAll_ShouldReturnCircuits(bool? used)
        {
            var circuits = new List<Circuit>
            {
                new Circuit(1, "Description"),
                new Circuit(2, "Description"),
                new Circuit(3, "Description")
            };

            circuitRepository.Setup(x => x.Query(used))
                .ReturnsAsync(circuits);

            var result = await instance.Query(used);

            result.Should().BeEquivalentTo(circuits);
        }

        [Fact]
        public async Task Get_ShouldReturnCircuit()
        {
            var circuit = new Circuit(1, "Description");

            circuitRepository.Setup(x => x.Get(1))
                .ReturnsAsync(circuit);

            var result = await instance.Get(1);

            result.Should().Be(circuit);
        }
    }
}
