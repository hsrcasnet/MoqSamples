using FluentAssertions;
using Moq;
using MoqSamples.Kitchen;
using Xunit;

namespace MoqSamples.Tests.Kitchen
{
    /// <summary>
    /// DEMO: Event mocking
    /// </summary>
    public class WaterBoilerTests
    {
        [Fact]
        public void ShouldTurnOnHeaterIfTooCold()
        {
            // Arrange
            var thermostatMock = new Mock<IThermostat>();
            var heaterMock = new Mock<IHeater>();

            var waterBoiler = new WaterBoiler(thermostatMock.Object, heaterMock.Object);

            // Act
            thermostatMock.Raise(t => t.TooCold += null, new ThermostatEventArgs(temperature: 10));

            // Assert
            waterBoiler.OnOffCycles.Should().Be(1);

            heaterMock.Verify(e => e.TurnOn(), Times.Exactly(1));
            heaterMock.Verify(e => e.TurnOff(), Times.Never);
        }

        [Fact]
        public void ShouldTurnOffHeaterIfTooHot()
        {
            // Arrange
            var thermostatMock = new Mock<IThermostat>();
            var heaterMock = new Mock<IHeater>();

            var waterBoiler = new WaterBoiler(thermostatMock.Object, heaterMock.Object);
            thermostatMock.Raise(t => t.TooCold += null, new ThermostatEventArgs(temperature: 10));

            // Act
            thermostatMock.Raise(t => t.TooHot += null, new ThermostatEventArgs(temperature: 35));

            // Assert
            waterBoiler.OnOffCycles.Should().Be(2);

            heaterMock.Verify(e => e.TurnOn(), Times.Exactly(1));
            heaterMock.Verify(e => e.TurnOff(), Times.Exactly(1));
        }
    }
}