using Nolan.HK.Application.Contracts.Dtos;
using Nolan.HK.Application.Contracts.Services;
using Nolan.HK.Domain.Entities;
using Nolan.Infra.Repository.IRepositories;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nolan.Application.Shared;
using Microsoft.Extensions.Options;
using Nolan.Domain.Shared.ConfigModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Nolan.HK.Application.Services
{
    public class UserService : AbstractAppService, IUserService
    {
        private readonly IEfBasicRepository<User> _userEntity;
        IOptions<JwtSetting> _settings;
        public UserService
            (
                IEfBasicRepository<User> userEntity
              , IOptions<JwtSetting> settings
            )
        {
            _userEntity = userEntity;
            _settings = settings;
        }

        public async Task<bool> CreateAsync(UserDto input)
        {
            var user = Mapper.Map<User>(input);
            user.Id = Guid.NewGuid();
            var exitUser = _userEntity.Where(p => p.Name == input.Name).FirstOrDefault();
            if (exitUser == null)
            {
                return await _userEntity.InsertAsync(user) > 0 ? true : false;
            }
            else
            {
                throw new Exception("this username have registered");
            }
        }

        public async Task<string> LoginAsync(UserDto input)
        {
            var user = _userEntity.Where(p => p.Name == input.Name && p.Password == input.Password).FirstOrDefault();
            var returnDto = Mapper.Map<UserDto>(user);
            if (returnDto != null)
            {
                Task<string> task = new Task<string>(() => GetToken(returnDto));
                task.Start();
                return await task;
            }
            else
            {
                throw new Exception("username or password is wrong");
            }
        }
        public async Task<bool> LoginTestAsync(UserDto input)
        {
           return  await _userEntity.AnyAsync(p => p.Name == input.Name && p.Password == input.Password);
        }
        public string GetToken(UserDto user)
        {
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
