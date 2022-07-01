using Microsoft.Extensions.Options;
using Moq;
using Nolan.Domain.Shared.ConfigModels;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Application.Services;
using Nolan.HK.Domain.Entities;
using Nolan.HK.MVC.Controllers;
using Nolan.Infra.Repository.IRepositories;
using NUnit.Framework;

namespace TestProject2
{
    public class Tests
    {
        public IEfBasicRepository<User> _userEntity;
        public IUserService _userService;
        public IOptions<JwtSetting> jwtSetting;
        [SetUp]
        public void Setup()
        {
            Mock<IUserService> mockObject = new Mock<IUserService>();
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Password = "123456";
            mockObject.Setup(m => m.LoginTestAsync(userDto)).Returns(1);
            _userService = mockObject.Object;
        }
         
        [Test]
        public void Test1()
        {
            _userEntity = SetUserRepository();
            jwtSetting = SetJwtSetting();
            _userService = new UserService(_userEntity,jwtSetting);
            UserController userController = new UserController(_userService);
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Password = "123456";
            var actualResult = userController.LoginTestAsync(userDto).Value;
            var expectedResult = 1;
            Assert.AreEqual(expectedResult, actualResult);
        }
        private IEfBasicRepository<User> SetUserRepository()
        {
            var mockRepo = new Mock<IEfBasicRepository<User>>(MockBehavior.Default);
            return mockRepo.Object;
        }
        private IOptions<JwtSetting> SetJwtSetting()
        {
            var mockRepo = new Mock<IOptions<JwtSetting>>(MockBehavior.Default);
            return mockRepo.Object;
        }
    }
}