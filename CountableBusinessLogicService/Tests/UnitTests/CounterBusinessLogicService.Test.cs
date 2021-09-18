using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Entities;
using Domain.Services;
using Integrations.CounterRestService;
using Integrations.UserRestService;
using Moq;
using NUnit.Framework;

namespace UnitTests
{

    [TestFixture]
    public class CounterServiceTest
    {
        private Mock<ICounterRestService> _counterService;
        private Mock<IUserRestService> _userService;
        private CounterBusinessLogicService _subjectUnderTest;
        private string _counterVersion;
        private byte[] _counterVersionBytes;

        [SetUp]
        public void Setup()
        {
            _counterVersion = "AAAAAAAAB9M=";
            _counterVersionBytes = Convert.FromBase64String(_counterVersion);
            _counterService = new Mock<ICounterRestService>();
            _userService = new Mock<IUserRestService>();
            _subjectUnderTest = new CounterBusinessLogicService(_counterService.Object, _userService.Object);
        }

        [Test]
        public void GetCounterWithPermissionCallsSuccessfully()
        {
            //Arrange
            ICounter returnObject = new Counter
            {
                Id = 1,
                Value = 100,
                Version = _counterVersionBytes
            };
            IUser user = new User(1, "Test", "Testesen", "Test@test.dk", new List<Permissions>() { Permissions.Read });

            var returnTask = Task.FromResult(returnObject);
            var userTask = Task.FromResult(user);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(userTask);
            _counterService.Setup(x => x.GetCounter()).Returns(returnTask);

            //Act
            var result = _subjectUnderTest.GetCounter(user.Id);

            //Assert
            Assert.That(result.IsCompletedSuccessfully, Is.True);
            Assert.That(result.Result.Id, Is.EqualTo(returnObject.Id));
            Assert.That(result.Result.Value, Is.EqualTo(returnObject.Value));
            Assert.That(result.Result.Version, Is.EqualTo(returnObject.Version));
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Exactly(1));
            _counterService.Verify(x => x.GetCounter(), Times.Exactly(1));
        }

        [Test]
        public void GetCounterWithoutPermissionThrowsException()
        {

        }

        [Test]
        public void IncrementWithPermissionCallsSuccessfully()
        {

        }
        [Test]
        public void IncrementWithoutPermissionThrowsException()
        {

        }
        [Test]
        public void DecrementWithPermissionCallsSuccessfully()
        {

        }
        [Test]
        public void DecrementWithoutPermissionThrowsException()
        {

        }
    }
}
