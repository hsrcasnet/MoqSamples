using FluentAssertions;
using Moq;
using Xunit;

namespace MoqSamples.Tests
{
    public class EventMockingTests
    {
        [Fact]
        public void ShouldTurnOnHeaterIfTooCold()
        {
            // Arrange
            var thermostatMock = new Mock<IThermostat>();
            var heaterMock = new Mock<IHeater>();

            var eventMocking = new EventMocking(thermostatMock.Object, heaterMock.Object);

            // Act
            thermostatMock.Raise(t => t.TooCold += null, new ThermostatEventArgs(temperature: 10));

            // Assert
            eventMocking.OnOffCycles.Should().Be(1);

            heaterMock.Verify(e => e.TurnOn(), Times.Exactly(1));
            heaterMock.Verify(e => e.TurnOff(), Times.Never);
        }

        [Fact]
        public void ShouldTurnOffHeaterIfTooHot()
        {
            // Arrange
            var thermostatMock = new Mock<IThermostat>();
            var heaterMock = new Mock<IHeater>();

            var eventMocking = new EventMocking(thermostatMock.Object, heaterMock.Object);
            thermostatMock.Raise(t => t.TooCold += null, new ThermostatEventArgs(temperature: 10));

            // Act
            thermostatMock.Raise(t => t.TooHot += null, new ThermostatEventArgs(temperature: 35));

            // Assert
            eventMocking.OnOffCycles.Should().Be(2);

            heaterMock.Verify(e => e.TurnOn(), Times.Exactly(1));
            heaterMock.Verify(e => e.TurnOff(), Times.Exactly(1));
        }
    }
}