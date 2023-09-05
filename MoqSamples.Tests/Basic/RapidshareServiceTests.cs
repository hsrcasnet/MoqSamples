using FluentAssertions;
using Moq;
using MoqSamples.Basic;
using Xunit;

namespace MoqSamples.Tests.Basic
{
    /// <summary>
    /// DEMO: Mocking methods of interfaces
    /// </summary>
    public class RapidshareServiceTests
    {
        [Fact]
        public void ShouldReturnDownloadExistsTrue_IfDownloadExists()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock.Setup(h => h.DownloadExists("2.0.0.0")).Returns(false);
            httpClientMock.Setup(h => h.DownloadExists("4.0.0.0")).Returns(true);
            IHttpClient httpClient = httpClientMock.Object;

            var rapidshareService = new RapidshareService(httpClient);

            // Act
            var downloadExists = rapidshareService.DownloadExists("4.0.0.0");

            // Assert
            downloadExists.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnDownloadExistsFalse_IfDownloadDoesNotExist()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            httpClientMock.Setup(h => h.DownloadExists("2.0.0.0")).Returns(true);
            httpClientMock.Setup(h => h.DownloadExists(It.Is<string>(s => s.StartsWith("2.")))).Returns(true);
            httpClientMock.Setup(h => h.DownloadExists(It.IsAny<string>())).Returns(false);

            var rapidshareService = new RapidshareService(httpClientMock.Object);

            // Act
            var downloadExists = rapidshareService.DownloadExists("2.1.0.0");

            // Assert
            downloadExists.Should().BeFalse();
        }
    }
}