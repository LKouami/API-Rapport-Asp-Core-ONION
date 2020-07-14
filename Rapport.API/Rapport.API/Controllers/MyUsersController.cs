using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rapport.Domaine.Dtos;
using Rapport.Domaine.Models;
using Rapport.Service;
using Rapport.Service.Implementations;

namespace Rapport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyUsersController : Controller
    {
        private readonly IMyUserService _myUserService;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<MyUserDto>> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _myUserService.AuthtUser(model.UserLogin, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        public MyUsersController(IMyUserService myUserService)
        {
            _myUserService = myUserService;
        }
        /// <summary>
        /// Return list of user
        /// </summary>
        /// <returns>IEnumerable of user</returns>
        [HttpGet]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<MyUserDto>>>> GetUser()
        {
            BaseResponseDto<IEnumerable<MyUserDto>> getResponse = await _myUserService.GetUsers();

            if (!getResponse.HasError || getResponse.Data != null)
            {
                return Ok(getResponse.Data);
            }
            else if (!getResponse.HasError || getResponse.Data == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(getResponse.Errors);
            }
        }

        /// <summary>
        /// Create a new user in data base
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponseDto<MyUserDto>>> AddUser(MyUser user)
        {
            BaseResponseDto<MyUserDto> getResponse = await _myUserService.CreateUser(user);

            if (!getResponse.HasError || getResponse.Data != null)
            {
                return Ok(getResponse.Data);
            }
            else if (!getResponse.HasError || getResponse.Data == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(getResponse.Errors);
            }
        }

        /// <summary>
        /// Retur a single user with id  
        /// </summary>
        /// <param name="id">Id is a Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById([FromRoute] string id)
        {
            Guid _id = Guid.Parse(id);

            BaseResponseDto<MyUserDto> getResponse = await _myUserService.GetUserById(_id);

            if (!getResponse.HasError || getResponse.Data != null)
            {
                return Ok(getResponse.Data);
            }
            else if (!getResponse.HasError || getResponse.Data == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(getResponse.Errors);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id">Is a Guid</param>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateComedian([FromRoute] string id, MyUser user)
        {
            Guid _id = Guid.Parse(id);
            await _myUserService.UpDateUser(_id, user);

            return Ok("User was update sucessfull");
        }

        /// <summary>
        /// Delete user with id
        /// </summary>
        /// <param name="id">Type is a guid</param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteComedian(Guid id)
        {
            BaseResponseDto<MyUserDto> getResponse = await _myUserService.DeleteUser(id);

            if (!getResponse.HasError)
            {
                return Ok(getResponse.Data);
            }
            else
            {
                return BadRequest(getResponse.Errors);
            }
        }
    }
}
