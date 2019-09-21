using FluentAssertions;
using Moq;
using Xunit;

namespace MoqSamples.Tests
{
    public class MockInterfaceTests
    {
        [Fact]
        public void ShouldReturnDownloadExistsTrue_IfDownloadExists()
        {
            // Arrange
            var loveThisFrameworkMock = new Mock<ILoveThisFramework>();
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0")).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("4.0.0.0")).Returns(false);
            ILoveThisFramework loveThisFramework = loveThisFrameworkMock.Object;

            var interfaceMocking = new InterfaceMocking(loveThisFramework);

            // Act
            var downloadExists = interfaceMocking.DownloadExists("4.0.0.0");

            // Assert
            downloadExists.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnDownloadExistsFalse_IfDownloadDoesNotExist()
        {
            // Arrange
            var loveThisFrameworkMock = new Mock<ILoveThisFramework>();
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0")).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists(It.Is<string>(s => s.StartsWith("2.")))).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists(It.IsAny<string>())).Returns(false);

            var interfaceMocking = new InterfaceMocking(loveThisFrameworkMock.Object);

            // Act
            var downloadExists = interfaceMocking.DownloadExists("2.1.0.0");

            // Assert
            downloadExists.Should().BeFalse();
        }
    }
}