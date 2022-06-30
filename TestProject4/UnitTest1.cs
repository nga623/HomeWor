using Moq;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.MVC.Controllers;
using System;
using Xunit;

namespace TestProject4
{
    public class UnitTest1
    {
        
        private IUserService prvGetMockExchangeRateFeed()
        {

            Mock<IUserService> mockObject = new Mock<IUserService>();
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Password = "123456";
            mockObject.Setup(m => m.LoginTestAsync(userDto)).Returns(500);
            return mockObject.Object;
        }
        [Fact]
        public void Test1()
        {
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Password = "123456";

            IUserService feed = this.prvGetMockExchangeRateFeed();
            UserController userController = new UserController(feed);
            var ss = userController.LoginTestAsync(userDto);
            int actualResult = userController.LoginTestAsync(userDto).Value;
            int expectedResult = 500;
            Assert.Equal(expectedResult, actualResult);

            // UserController userController = new UserController(_userService);
            // UserDto userDto = new UserDto();
            // userDto.Name = "admin";
            //  userDto.Password = "123456";
            //var actualResult =    userController.LoginTestAsync(userDto).Result.Value;
            ////  var actualResult=  _userService.LoginTestAsync(userDto).Result;
            // var expectedResult = 1;
            // Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
