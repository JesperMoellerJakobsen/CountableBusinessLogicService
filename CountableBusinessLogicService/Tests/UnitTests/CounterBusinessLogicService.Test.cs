using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain.Model.Config;
using Domain.Model.Entities;
using Domain.Services;
using Integrations.CounterRestService;
using Integrations.UserRestService;
using Moq;
using NUnit.Framework;
using Action = Domain.Model.Entities.Action;

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
        private ICounter _returnObject;
        private IUser _user;

        [SetUp]
        public void Setup()
        {
            _counterVersion = "AAAAAAAAB9M=";
            _counterVersionBytes = Convert.FromBase64String(_counterVersion);
            _counterService = new Mock<ICounterRestService>();
            _userService = new Mock<IUserRestService>();
            _subjectUnderTest = new CounterBusinessLogicService(_counterService.Object, _userService.Object);
            _returnObject = new Counter
            {
                Id = 1,
                Value = 100,
                Version = _counterVersionBytes
            };
            _user = new User(1, "Test", "Testesen", "Test@test.dk", new List<Action>());
        }

        [Test]
        public async Task GetCounterWithPermissionCallsSuccessfully()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Read);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));
            _counterService.Setup(x => x.GetCounter()).Returns(Task.FromResult(_returnObject));

            //Act
            var result = await _subjectUnderTest.GetCounter(_user.Id);

            //Assert
            Assert.That(result.Id, Is.EqualTo(_returnObject.Id));
            Assert.That(result.Value, Is.EqualTo(_returnObject.Value));
            Assert.That(result.Version, Is.EqualTo(_returnObject.Version));
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.GetCounter(), Times.Once);
        }

        [Test]
        public void GetCounterWithoutPermissionThrowsException()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Write);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));

            //Act
            ICounter result = null;
            Assert.ThrowsAsync<ActionNotPermittedException>(async () => result = await _subjectUnderTest.GetCounter(_user.Id));

            //Assert
            Assert.That(result, Is.Null);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.GetCounter(), Times.Never);
        }

        [Test]
        public async Task IncrementWithPermissionCallsSuccessfully()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Write);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));
            _counterService.Setup(x => x.TryIncrement(It.IsAny<byte[]>())).Returns(Task.FromResult(true));

            //Act
            var result = await _subjectUnderTest.TryIncrement(_user.Id, _counterVersionBytes);

            //Assert
            Assert.That(result, Is.True);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.TryIncrement(It.IsAny<byte[]>()), Times.Once);
        }
        [Test]
        public void IncrementWithoutPermissionThrowsException()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Read);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));

            //Act
            bool result = false;
            Assert.ThrowsAsync<ActionNotPermittedException>(async () => result = await _subjectUnderTest.TryIncrement(_user.Id, _counterVersionBytes));

            //Assert
            Assert.That(result, Is.False);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.TryIncrement(It.IsAny<byte[]>()), Times.Never);
        }

        [Test]
        public async Task DecrementWithPermissionCallsSuccessfully()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Write);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));
            _counterService.Setup(x => x.TryDecrement(It.IsAny<byte[]>())).Returns(Task.FromResult(true));
            _counterService.Setup(x => x.GetCounter()).Returns(Task.FromResult(_returnObject));

            //Act
            var result = await _subjectUnderTest.TryDecrement(_user.Id, _counterVersionBytes);

            //Assert
            Assert.That(result, Is.True);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.TryDecrement(It.IsAny<byte[]>()), Times.Once);
            _counterService.Verify(x=>x.GetCounter(), Times.Once);
        }

        [Test]
        public void DecrementWithoutPermissionThrowsException()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Read);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));

            //Act
            bool result = false;
            Assert.ThrowsAsync<ActionNotPermittedException>(async () => result = await _subjectUnderTest.TryDecrement(_user.Id, _counterVersionBytes));

            //Assert
            Assert.That(result, Is.False);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.TryDecrement(It.IsAny<byte[]>()), Times.Never);
        }

        [Test]
        public void DecrementToNegativeWithoutPermission()
        {
            //Arrange
            _user.ActionsAllowed.Add(Action.Write);
            _userService.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(Task.FromResult(_user));
            _counterService.Setup(x => x.GetCounter()).Returns(Task.FromResult(_returnObject));
            _returnObject.Value = 0;

            //Act
            bool result = false;
            Assert.ThrowsAsync<ActionNotPermittedException>(async () => result = await _subjectUnderTest.TryDecrement(_user.Id, _counterVersionBytes));

            //Assert
            Assert.That(result, Is.False);
            _userService.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Once);
            _counterService.Verify(x => x.GetCounter(), Times.Once());
            _counterService.Verify(x => x.TryDecrement(It.IsAny<byte[]>()), Times.Never);
        }
    }
}
