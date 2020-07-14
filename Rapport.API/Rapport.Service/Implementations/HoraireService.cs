using Microsoft.Extensions.Logging;
using Rapport.Domaine.Dtos;
using Rapport.Domaine.Interfaces;
using Rapport.Domaine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Service.Implementations
{
   public class HoraireService : IHoraireService
    {

        private readonly ILogger<HoraireService> _logger;
        private readonly IRepository<Horaire> _horaireRepository;

        public HoraireService(ILogger<HoraireService> logger, IRepository<Horaire> horaireRepository)
        {
            _logger = logger;
            _horaireRepository = horaireRepository;
        }
        public async Task<BaseResponseDto<HoraireDto>> CreateHoraire(Horaire horaire)
        {
            BaseResponseDto<HoraireDto> getResponse = new BaseResponseDto<HoraireDto>();
            try
            {
                await _horaireRepository.CreateAsync(horaire);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<HoraireDto>> DeleteHoraire(Guid Id)
        {
            BaseResponseDto<HoraireDto> getResponse = new BaseResponseDto<HoraireDto>();
            try
            {
               if(Id != null)
                {
                    Horaire horaire = await _horaireRepository.GetAsync(Id);
                    await _horaireRepository.DeleteAsync(horaire);
                }
                else
                {
                    getResponse.Errors.Add("Horaire can't be delete.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<HoraireDto>> GetHoraireById(Guid Id)
        {
            BaseResponseDto<HoraireDto> getResponse = new BaseResponseDto<HoraireDto>();
            try
            {
                Horaire horaire = await _horaireRepository.GetAsync(Id);
                if(horaire != null)
                {
                    getResponse.Data = new HoraireDto
                    {
                        Id = horaire.Id,
                        MyUserId = horaire.MyUserId,
                        DateDuJourHor = horaire.DateDuJourHor,
                        HeureArriveHor = horaire.HeureArriveHor,
                        HeureDepartHor = horaire.HeureDepartHor
                    };
                    
                }
                else
                {
                    getResponse.Errors.Add("Horaire can't be ");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<IEnumerable<HoraireDto>>> GetHoraires()
        {
            BaseResponseDto<IEnumerable<HoraireDto>> getResponse = new BaseResponseDto<IEnumerable<HoraireDto>>();
            try
            {
                getResponse.Data =
                    (await _horaireRepository.GetAllAsync()).Select(h => new HoraireDto()
                    { 
                        Id = h.Id,
                        MyUserId = h.MyUserId,
                        DateDuJourHor = h.DateDuJourHor,
                        HeureArriveHor = h.HeureArriveHor,
                        HeureDepartHor = h.HeureDepartHor
                    });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<HoraireDto>> UpdateHoraire(Guid Id, Horaire horaire)
        {
            BaseResponseDto<HoraireDto> getResponse = new BaseResponseDto<HoraireDto>();
            try
            {
                if(Id != null)
                {
                    Horaire oldHoraire = await _horaireRepository.GetAsync(Id);
                    if(oldHoraire != null)
                    {
                        oldHoraire.MyUserId = horaire.MyUserId;
                        oldHoraire.DateDuJourHor = horaire.DateDuJourHor;
                        oldHoraire.HeureArriveHor = horaire.HeureArriveHor;
                        oldHoraire.HeureDepartHor = horaire.HeureDepartHor;

                        await _horaireRepository.UpdateAsync(oldHoraire);
                    }
                }
                else
                {
                    return null;
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }
    }
}
