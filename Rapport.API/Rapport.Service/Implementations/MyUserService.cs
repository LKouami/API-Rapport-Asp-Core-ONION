using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rapport.Domaine.Dtos;
using Rapport.Domaine.Interfaces;
using Rapport.Domaine.Models;
using Rapport.Service.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Service.Implementations
{
   public class MyUserService : IMyUserService
    {

        private readonly ILogger<MyUserService> _logger;
        private readonly IRepository<MyUser> _userRepository;
        private readonly AppSettings _appSettings;

        public MyUserService(ILogger<MyUserService> logger, IRepository<MyUser> userRepository, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<BaseResponseDto<MyUserDto>> CreateUser(MyUser user)
        {
            BaseResponseDto<MyUserDto> getResponse = new BaseResponseDto<MyUserDto>();
            try
            {
                await _userRepository.CreateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<MyUserDto>> DeleteUser(Guid Id)
        {
            BaseResponseDto<MyUserDto> getResponse = new BaseResponseDto<MyUserDto>();
            try
            {
                if (Id != null)
                {
                    MyUser myUser = await _userRepository.GetAsync(Id);
                    await _userRepository.DeleteAsync(myUser);
                }
                else
                {
                    getResponse.Errors.Add("User can't be delete.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }

            return getResponse;
        }

        public async Task<BaseResponseDto<MyUserDto>> GetUserById(Guid Id)
        {
            BaseResponseDto<MyUserDto> getResponse = new BaseResponseDto<MyUserDto>();

            try
            {
                MyUser myUser = await _userRepository.GetAsync(Id);

                if (myUser != null)
                {
                    getResponse.Data = new MyUserDto
                    {
                        Id = myUser.Id,
                        
                        LastName = myUser.LastName,
                        FirstName = myUser.FirstName,
                      
                        Phone = myUser.Phone,
                        Email = myUser.Email,
                        UserLogin = myUser.UserLogin,
                        Password = myUser.Password,
                        
                        ModifiedAt = myUser.ModifiedAt,
                        CreatedAt = myUser.CreatedAt
                    };
                }
                else
                {
                    getResponse.Errors.Add("User not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }

            return getResponse;
        }

        public async Task<BaseResponseDto<IEnumerable<MyUserDto>>> GetUsers()
        {
            BaseResponseDto<IEnumerable<MyUserDto>> getResponse = new BaseResponseDto<IEnumerable<MyUserDto>>();
            try
            {
                getResponse.Data =
                  (await _userRepository.GetAllAsync()).Select(u => new MyUserDto()
                  {
                      Id = u.Id,
                      
                      LastName = u.LastName,
                      FirstName = u.FirstName,
                     
                      Phone = u.Phone,
                      Email = u.Email,
                      UserLogin = u.UserLogin,
                      Password = u.Password,
                      
                      ModifiedAt = u.ModifiedAt,
                      CreatedAt = u.CreatedAt
                  });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }

            return getResponse;
        }

        public async Task<BaseResponseDto<MyUserDto>> UpDateUser(Guid Id, MyUser user)
        {
            BaseResponseDto<MyUserDto> getResponse = new BaseResponseDto<MyUserDto>();
            try
            {
                if (Id != null)
                {
                    MyUser oldUser = await _userRepository.GetAsync(Id);
                    if (oldUser != null)
                    {
                        
                        oldUser.LastName = user.LastName;
                        oldUser.FirstName = user.FirstName;
                       
                        oldUser.Phone = user.Phone;
                        oldUser.Email = user.Email;
                        oldUser.UserLogin = user.UserLogin;
                        oldUser.Password = user.Password;
                        oldUser.Token = user.Token;
                        
                        oldUser.ModifiedAt = DateTime.Now;

                        await _userRepository.UpdateAsync(oldUser);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    getResponse.Errors.Add("User type can't be update.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }

            return getResponse;
        }

        public async Task<BaseResponseDto<MyUserDto>> AuthtUser(string UserLogin, string Password)
        {
            BaseResponseDto<MyUserDto> getResponse = new BaseResponseDto<MyUserDto>();
            try
            {
                MyUser user = await _userRepository.Authentification(UserLogin, Password);

                if (user == null)
                    return null;

                getResponse.Data = new MyUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    Token = user.Token
                };

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                getResponse.Data.Token = tokenHandler.WriteToken(token);

                return getResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }
    }
}
