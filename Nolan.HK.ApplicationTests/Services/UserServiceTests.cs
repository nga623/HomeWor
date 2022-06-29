using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nolan.HK.Application.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        public readonly IUserService _userService;  
        public UserServiceTests(IUserService userService)
        {
            _userService = userService;
        }
        [TestMethod()]
        public void LoginAsyncTest()
        {
            UserDto userDto = new UserDto();
            userDto.Name = "admin";
            userDto.Name = "gl@123321";
            Assert.Equals(true,   _userService.LoginTestAsync(userDto));
        }
    }
}