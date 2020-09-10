using FluentAssertions;
using Moq;
using Xunit;

namespace MoqSamples.Tests.InterfaceMocking
{
    public class RapidshareServiceTests
    {
        [Fact]
        public void ShouldReturnDownloadExistsTrue_IfDownloadExists()
        {
            // Arrange
            var loveThisFrameworkMock = new Mock<ILoveThisFramework>();
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0")).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("4.0.0.0")).Returns(false);
            ILoveThisFramework loveThisFramework = loveThisFrameworkMock.Object;

            var rapidshareService = new RapidshareService(loveThisFramework);

            // Act
            var downloadExists = rapidshareService.DownloadExists("4.0.0.0");

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

            var rapidshareService = new RapidshareService(loveThisFrameworkMock.Object);

            // Act
            var downloadExists = rapidshareService.DownloadExists("2.1.0.0");

            // Assert
            downloadExists.Should().BeFalse();
        }
    }
}