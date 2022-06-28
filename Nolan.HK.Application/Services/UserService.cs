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
using Nolan.WebApi.Shared.ResultModel;
using Microsoft.Extensions.Options;
using Nolan.Domain.Shared.ConfigModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Nolan.HK.Application.Services
{

    public class UserService : AbstractAppService, IUserService
    {
        private readonly IEfBasicRepository<User> _User;
        IOptions<JwtSetting> _settings;
        public UserService(
            IEfBasicRepository<User> user
           , IOptions<JwtSetting> settings
            )
        {
            _User = user;
            _settings = settings;
        }
        public async Task<int> CreateAsync(UserDto input)
        {
            var user = Mapper.Map<User>(input);
            user.Id = Guid.NewGuid();
            var hasUser = _User.Where(p => p.Name == input.Name).FirstOrDefault();
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

        public async Task<string> LoginAsync(UserDto input)
        {
            var listUser = _User.Where(p => p.Id != Guid.Empty).ToList();
            await Task.Delay(100);
            var user = listUser.Where(p => p.Name == input.Name && p.Password == input.Password).FirstOrDefault();
            var returnDto = Mapper.Map<UserDto>(user);
            if (returnDto != null)
            {
               return GetToken(returnDto);
            }
            else
            {
                return string.Empty;
            }
            
             

        }
        public string GetToken(UserDto user)
        {
            //创建用户身份标识，可按需要添加更多信息
            var claims = new Claim[]
            {
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim("name", user.Name.ToString(), ClaimValueTypes.Integer32),
    new Claim("UserTypeEnum", user.UserTypeEnum.ToString(),ClaimValueTypes.Integer32)
            };
            var _jwtSetting = new JwtSetting();
            _jwtSetting.SecurityKey = _settings.Value.SecurityKey;
            _jwtSetting.Issuer = _settings.Value.Issuer;
            _jwtSetting.Audience = _settings.Value.Audience;
            var algorithm = SecurityAlgorithms.HmacSha256;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecurityKey));
            var signingCredentials = new SigningCredentials(secretKey, algorithm);
            //创建令牌
            var token = new JwtSecurityToken(
              issuer: _jwtSetting.Issuer,
              audience: _jwtSetting.Audience,
              signingCredentials: signingCredentials,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddSeconds(10000000)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
