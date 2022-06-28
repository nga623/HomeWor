using Nolan.HK.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nolan.HK.Application.Contracts.Services
{
     
    public interface IUserService
    {
        Task<bool> CreateAsync(UserDto input);
        Task<string> LoginAsync(UserDto input);
       // UserDto GetAsync(UserDto input);
    }
}
