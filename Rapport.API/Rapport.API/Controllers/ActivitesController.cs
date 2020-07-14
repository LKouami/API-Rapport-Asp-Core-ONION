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
    public class ActivitesController : Controller
    {
        private readonly IActiviteService _activiteService;

        public ActivitesController(IActiviteService activiteService)
        {
            _activiteService = activiteService;
        }
        /// <summary>
        /// Return list of activity
        /// </summary>
        /// <returns>IEnumerable of Activity</returns>
        [HttpGet]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<ActiviteDto>>>> GetActivities()
        {
            BaseResponseDto<IEnumerable<ActiviteDto>> getResponse = await _activiteService.GetActivites();
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
        /// Create a new activity in data base
        /// </summary>
        /// <param name="activite"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponseDto<ActiviteDto>>> AddActivity(Activite activite)
        {
            BaseResponseDto<ActiviteDto> getResponse = await _activiteService.CreateActivite(activite);

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
        /// Return a single activity with id  
        /// </summary>
        /// <param name="id">Id is a Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetActivityById([FromRoute] string id)
        {
            Guid _id = Guid.Parse(id);

            BaseResponseDto<ActiviteDto> getResponse = await _activiteService.GetActiviteById(_id);

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
        /// Update activity
        /// </summary>
        /// <param name="id">Is a Guid</param>
        /// <param name="activite"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateActivity([FromRoute] string id, Activite activite)
        {
            Guid _id = Guid.Parse(id);
            await _activiteService.UpdateActivite(_id, activite);

            return Ok("Activity was updated sucessfull");
        }

        /// <summary>
        /// Delete activity with id
        /// </summary>
        /// <param name="IdAct">Type is a guid</param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteActivity(Guid IdAct)
        {
            BaseResponseDto<ActiviteDto> getResponse = await _activiteService.DeleteActivite(IdAct);

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
