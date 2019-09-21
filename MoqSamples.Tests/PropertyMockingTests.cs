using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace MoqSamples.Tests
{
    public class PropertyMockingTests
    {
        [Fact]
        public void ShouldDeleteRetiredPersonsByAge()
        {
            // Arrange
            var testPersons = new List<Person>
            {
                new Person { Name = "Martin", Birthdate = new DateTime(2010, 12, 31) },
                new Person { Name = "Thomas", Birthdate = new DateTime(1986, 07, 11) },
                new Person { Name = "Marcel", Birthdate = new DateTime(1940, 01, 01) },
                new Person { Name = "Joseph", Birthdate = new DateTime(1920, 01, 01) },
            };

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.SetupGet(d => d.Now).Returns(new DateTime(2019, 01, 01));

            var personRepositoryMock = new Mock<IPersonRepository>();
            personRepositoryMock.Setup(r => r.GetPersons()).Returns(testPersons);
            personRepositoryMock.Setup(r => r.DeletePerson(It.IsAny<Person>()));

            var propertyMocking = new PropertyMocking(dateTimeMock.Object, personRepositoryMock.Object);

            // Act
            propertyMocking.DeleteRetiredPersons(age: 65);

            // Assert
            personRepositoryMock.Verify(r => r.GetPersons(), Times.Once);
            personRepositoryMock.Verify(r => r.DeletePerson(It.IsAny<Person>()), Times.Exactly(2));
            personRepositoryMock.Verify(r => r.DeletePerson(It.Is<Person>(p => p.Name == "Marcel")), Times.Exactly(1));
            personRepositoryMock.Verify(r => r.DeletePerson(It.Is<Person>(p => p.Name == "Joseph")), Times.Exactly(1));
        }

        [Fact]
        public void ShouldDeleteRetiredPersonsByAge_UsingVerifications()
        {
            // Arrange
            var testPersons = new List<Person>
            {
                new Person { Name = "Martin", Birthdate = new DateTime(2010, 12, 31) },
                new Person { Name = "Thomas", Birthdate = new DateTime(1986, 07, 11) },
                new Person { Name = "Marcel", Birthdate = new DateTime(1940, 01, 01) },
                new Person { Name = "Joseph", Birthdate = new DateTime(1920, 01, 01) },
            };

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.SetupGet(d => d.Now).Returns(new DateTime(2019, 01, 01));

            var personRepositoryMock = new Mock<IPersonRepository>();
            personRepositoryMock.Setup(r => r.GetPersons()).Returns(testPersons).Verifiable();
            personRepositoryMock.Setup(r => r.DeletePerson(It.Is<Person>(p => p.Name == "Marcel"))).Verifiable();
            personRepositoryMock.Setup(r => r.DeletePerson(It.Is<Person>(p => p.Name == "Joseph"))).Verifiable();

            // DEMO1: Setup an unmatched method call 
            personRepositoryMock.Setup(r => r.DeletePerson(It.Is<Person>(p => p.Name == "Test")));

            var propertyMocking = new PropertyMocking(dateTimeMock.Object, personRepositoryMock.Object);

            // Act
            propertyMocking.DeleteRetiredPersons(age: 65);

            // Assert
            // DEMO1: Verify() checks all Setups marked with .Verifiable()
            personRepositoryMock.VerifyAll();

            // DEMO2: VerifyAll() checks all Setups - regardless if they're marked with .Verifiable()
            personRepositoryMock.Verify();

            // DEMO3: VerifyNoOtherCalls fails if unverified invocations happened
            personRepositoryMock.VerifyNoOtherCalls();
        }
    }
}