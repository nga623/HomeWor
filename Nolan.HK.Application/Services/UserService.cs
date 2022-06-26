using AutoMapper;
using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nolan.Infra.Mapper;
using Nolan.Application.Shared;
using Nolan.HK.Domain.Services;
using Nolan.HK.Domain.Enum;

namespace Nolan.HK.Application.Services
{

    public class UserService : AbstractAppService, IUserService
    {
        private readonly IEfBasicRepository<User> _User;
        public UserService(IEfBasicRepository<User> user)
        {
            _User = user;
        }
        public async Task<int> CreateAsync(UserDto input)
        {
            var user = Mapper.Map<User>(input);
            user.Id = Guid.NewGuid();
            var listUser = _User.Where(p => p.Id != Guid.Empty).ToList();
            var hasUser = listUser.Where(p => p.Name == input.Name).FirstOrDefault();
            if (hasUser == null)
            {
                return await _User.InsertAsync(user);
            }
            else
            {
                return 0;
            }

        }

        public UserDto GetAsync(UserDto input)
        {
            var listUser = _User.Where(p => p.Id != Guid.Empty).ToList();
            var user = listUser.Where(p => p.Name == input.Name).FirstOrDefault();
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(UserDto input)
        {
            var listUser = _User.Where(p => p.Id != Guid.Empty).ToList();
            await Task.Delay(100);
            var user = listUser.Where(p => p.Name == input.Name && p.Password == input.Password).FirstOrDefault();

            return Mapper.Map<UserDto>(user);

        }
    }
}
