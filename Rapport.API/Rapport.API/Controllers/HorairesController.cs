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
    public class HorairesController : Controller
    {
        private readonly IHoraireService _horaireService;

        public HorairesController(IHoraireService horaireService)
        {
            _horaireService = horaireService;
        }
        /// <summary>
        /// Return list of Horaire
        /// </summary>
        /// <returns>IEnumerable of Horaire</returns>
        [HttpGet]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<HoraireDto>>>> GetActivities()
        {
            BaseResponseDto<IEnumerable<HoraireDto>> getResponse = await _horaireService.GetHoraires();
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
        /// Create a new Horaire in data base
        /// </summary>
        /// <param name="horaire"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseResponseDto<HoraireDto>>> AddHoraire(Horaire horaire)
        {
            BaseResponseDto<HoraireDto> getResponse = await _horaireService.CreateHoraire(horaire);

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
        /// Return a single horaire with id  
        /// </summary>
        /// <param name="id">Id is a Guid</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetHoraireById([FromRoute] string id)
        {
            Guid _id = Guid.Parse(id);

            BaseResponseDto<HoraireDto> getResponse = await _horaireService.GetHoraireById(_id);

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
        /// Update horaire
        /// </summary>
        /// <param name="id">Is a Guid</param>
        /// <param name="horaire"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateHoraire([FromRoute] string id, Horaire horaire)
        {
            Guid _id = Guid.Parse(id);
            await _horaireService.UpdateHoraire(_id, horaire);

            return Ok("Horaire was updated sucessfull");
        }

        /// <summary>
        /// Delete horaire with id
        /// </summary>
        /// <param name="id">Type is a guid</param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ActionResult<string>> DeleteHoraire(Guid id)
        {
            BaseResponseDto<HoraireDto> getResponse = await _horaireService.DeleteHoraire(id);

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
