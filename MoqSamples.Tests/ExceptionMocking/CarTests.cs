using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace MoqSamples.Tests.ExceptionMocking
{
    public class CarTests
    {
        [Fact]
        public void ShouldStartTheEngineInFirstAttempt()
        {
            // Arrange
            var engineMock = new Mock<IEngine>();
            engineMock.SetupSequence(e => e.StartUp());

            var car = new Car(engineMock.Object);

            // Act
            car.StartEngine();

            // Assert
            engineMock.Verify(e => e.StartUp(), Times.Exactly(1));
        }

        [Fact]
        public void ShouldStartTheEngineInThirdAttempt()
        {
            // Arrange
            var engineMock = new Mock<IEngine>();
            engineMock.SetupSequence(e => e.StartUp())
                .Throws(new EngineStartException("Ignition failure"))
                .Throws(new EngineStartException("Fuel pump not operational"));

            var car = new Car(engineMock.Object);

            // Act
            car.StartEngine();

            // Assert
            engineMock.Verify(e => e.StartUp(), Times.Exactly(3));
        }

        [Fact]
        public void ShouldThrowExceptionIfEngineFailsToStartThreeTimes()
        {
            // Arrange
            var engineMock = new Mock<IEngine>();
            engineMock.SetupSequence(e => e.StartUp())
                .Throws(new EngineStartException("Ignition failure"))
                .Throws(new EngineStartException("Fuel pump not operational"))
                .Throws(new EngineStartException("Exhaust gas recirculation system failure"));

            var car = new Car(engineMock.Object);

            // Act
            Action action = () => car.StartEngine();

            // Assert
            action.Should().Throw<EngineStartException>().Which.Message.Should().Be("Exhaust gas recirculation system failure");

            engineMock.Verify(e => e.StartUp(), Times.Exactly(3));
        }
    }
}