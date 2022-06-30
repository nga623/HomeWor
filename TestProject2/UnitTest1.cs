using Microsoft.AspNetCore.Mvc;
using Moq;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Application.Services;
using Nolan.HK.Domain.Entities;
using Nolan.HK.MVC.Controllers;
using Nolan.Infra.Repository.IRepositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TestProject2
{
    public class Tests
    {
        public IEfBasicRepository<User> _userEntity;
        public IUserService _userService;
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
        private IUserService prvGetMockExchangeRateFeed()
        {

            Mock<IUserService> mockObject = new Mock<IUserService>();
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Password = "123456";
            mockObject.Setup(m => m.LoginTestAsync(userDto)).Returns(500);
            return mockObject.Object;
        }
        [Test]
        public void Test1()
        {
            
          //  IUserService feed = this.prvGetMockExchangeRateFeed();
            _userEntity = SetUpProductRepository();
            _userService = new UserService(_userEntity);
          // UserController userController = new UserController(feed);
           // int actualResult = userController.LoginTestAsync(userDto).Value;
          //  int expectedResult = 500;
           // Assert.AreEqual(expectedResult, actualResult);

              UserController userController = new UserController(_userService);
             UserDto userDto = new UserDto();
              userDto.Name = "admin";
               userDto.Password = "123456";
             var actualResult =    userController.LoginTestAsync(userDto).Value;
            ////  var actualResult=  _userService.LoginTestAsync(userDto).Result;
              var expectedResult = 1;
             Assert.AreEqual(expectedResult, actualResult);
        }
        private IEfBasicRepository<User> SetUpProductRepository()
        {
            var mockRepo = new Mock<IEfBasicRepository<User>>(MockBehavior.Default);
            return mockRepo.Object;
        }
    }
}