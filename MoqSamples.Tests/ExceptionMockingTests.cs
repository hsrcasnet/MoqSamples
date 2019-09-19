using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace MoqSamples.Tests
{
    public class ExceptionMockingTests
    {
        [Fact]
        public void ShouldStartTheEngineInFirstAttempt()
        {
            // Arrange
            var engineMock = new Mock<IEngine>();
            engineMock.SetupSequence(e => e.StartUp());

            var exceptionMocking = new ExceptionMocking(engineMock.Object);

            // Act
            exceptionMocking.StartEngine();

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

            var exceptionMocking = new ExceptionMocking(engineMock.Object);

            // Act
            exceptionMocking.StartEngine();

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

            var exceptionMocking = new ExceptionMocking(engineMock.Object);

            // Act
            Action action = () => exceptionMocking.StartEngine();

            // Assert
            action.Should().Throw<EngineStartException>().Which.Message.Should().Be("Exhaust gas recirculation system failure");

            engineMock.Verify(e => e.StartUp(), Times.Exactly(3));
        }
    }
}