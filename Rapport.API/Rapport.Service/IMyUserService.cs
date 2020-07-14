using Rapport.Domaine.Dtos;
using Rapport.Domaine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Service
{
    public interface IMyUserService
    {
        Task<BaseResponseDto<MyUserDto>> CreateUser(MyUser user);
        Task<BaseResponseDto<MyUserDto>> GetUserById(Guid Id);
        Task<BaseResponseDto<IEnumerable<MyUserDto>>> GetUsers();
        Task<BaseResponseDto<MyUserDto>> UpDateUser(Guid Id, MyUser user);
        Task<BaseResponseDto<MyUserDto>> DeleteUser(Guid Id);
        Task<BaseResponseDto<MyUserDto>> AuthtUser(string email, string password);

    }
}
