using System;
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
            var loveThisFrameworkMock = new Mock<ILoveThisFramework>(MockBehavior.Loose);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0")).Returns(true);

            var interfaceMocking = new InterfaceMocking(loveThisFrameworkMock.Object);

            // Act
            var downloadExists = interfaceMocking.DownloadExists("2.0.0.0");

            // Assert
            downloadExists.Should().BeTrue();
        }
        
        [Fact]
        public void ShouldReturnDownloadExistsFalse_IfDownloadDoesNotExist()
        {
            // Arrange
            var loveThisFrameworkMock = new Mock<ILoveThisFramework>(MockBehavior.Loose);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0")).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists(It.Is<string>(s => s.StartsWith("2.")))).Returns(true);
            loveThisFrameworkMock.Setup(framework => framework.DownloadExists(It.IsAny<string>())).Returns(false);

            var interfaceMocking = new InterfaceMocking(loveThisFrameworkMock.Object);

            // Act
            var downloadExists = interfaceMocking.DownloadExists("2.1.0.0");

            // Assert
            downloadExists.Should().BeFalse();
        }

        //[Fact]
        //public void ShouldReturnDownloadExistsFalse_IfDownloadDoesNotExist2()
        //{
        //    // Arrange
        //    var loveThisFrameworkMock = new Mock<ILoveThisFramework>(MockBehavior.Loose);
        //    loveThisFrameworkMock.SetupGet(f => f.Now).Returns(new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Local));
        //    loveThisFrameworkMock.Setup(framework => framework.DownloadExists("2.0.0.0"))
        //        .Returns(new Person { Name = "Thomas" });
        //    // Hand mock.Object as a collaborator and exercise it, 
        //    // like calling methods on it...

        //    ILoveThisFramework lovable = loveThisFrameworkMock.Object;
        //    Person download = lovable.DownloadExists("2.0.0.0");
        //    var p = lovable.Now;

        //    //        download = lovable.DownloadExists("2.0.0.0");
        //    // Verify that the given method was indeed called with the expected value
        //    loveThisFrameworkMock.Verify(framework => framework.DownloadExists(It.Is<string>(s => s.Contains("2"))), Times.Once);

        //    download = lovable.DownloadExists("2.0.0.1");

        //    var interfaceMocking = new InterfaceMocking(loveThisFrameworkMock.Object);

        //    // Act
        //    var downloadExists = interfaceMocking.DownloadExists("2.0.0.0");

        //    // Assert
        //    downloadExists.Should().BeTrue();
        //}

        [Fact]
        public void Test1()
        {
            // Arrange

            // Act

            // Assert

        }
    }
}