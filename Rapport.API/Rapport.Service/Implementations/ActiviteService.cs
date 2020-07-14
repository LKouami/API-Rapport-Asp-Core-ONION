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
    public class ActiviteService : IActiviteService
    {
        private readonly ILogger<ActiviteService> _logger;
        private readonly IRepository<Activite> _activiteRepository;

        public ActiviteService(ILogger<ActiviteService> logger, IRepository<Activite> activiteRepository)
        {
            _logger = logger;
            _activiteRepository = activiteRepository;
        }
        public async Task<BaseResponseDto<ActiviteDto>> CreateActivite(Activite activite)
        {
            BaseResponseDto<ActiviteDto> getResponse = new BaseResponseDto<ActiviteDto>();
            try
            {
                await _activiteRepository.CreateAsync(activite);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<ActiviteDto>> DeleteActivite(Guid IdAct)
        {
            BaseResponseDto<ActiviteDto> getResponse = new BaseResponseDto<ActiviteDto>();
            try
            {
                if(IdAct != null)
                {
                    Activite activite = await _activiteRepository.GetAsync(IdAct);
                    await _activiteRepository.DeleteAsync(activite);
                }
                else
                {
                    getResponse.Errors.Add("Activity can't be delete.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<ActiviteDto>> GetActiviteById(Guid Id)
        {
            BaseResponseDto<ActiviteDto> getResponse = new BaseResponseDto<ActiviteDto>();
            try
            {
                Activite activite = await _activiteRepository.GetAsync(Id);

                if(activite != null)
                {
                    getResponse.Data = new ActiviteDto
                    {
                        Id = activite.Id,
                        MyUserId = activite.MyUserId,
                        ContenuAct = activite.ContenuAct,
                        DateAct = activite.DateAct
                    };
                }
                else
                {
                    getResponse.Errors.Add("Activity not found. ");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;

        }

        public async Task<BaseResponseDto<IEnumerable<ActiviteDto>>> GetActivites()
        {
            BaseResponseDto<IEnumerable<ActiviteDto>> getResponse = new BaseResponseDto<IEnumerable<ActiviteDto>>();
            try
            {
                getResponse.Data =
                    (await _activiteRepository.GetAllAsync()).Select(a => new ActiviteDto()
                    {
                        Id = a.Id,
                        MyUserId = a.MyUserId,
                        ContenuAct = a.ContenuAct,
                        DateAct = a.DateAct

                    });

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                getResponse.Errors.Add(ex.Message);
            }
            return getResponse;
        }

        public async Task<BaseResponseDto<ActiviteDto>> UpdateActivite(Guid Id, Activite activite)
        {
            BaseResponseDto<ActiviteDto> getResponse = new BaseResponseDto<ActiviteDto>();
            try
            {
                if (Id != null)
                {
                    Activite oldActivite = await _activiteRepository.GetAsync(Id);
                    if (oldActivite != null)
                    {
                        oldActivite.MyUserId = activite.MyUserId;
                        oldActivite.ContenuAct = activite.ContenuAct;
                        oldActivite.DateAct = activite.DateAct;

                        await _activiteRepository.UpdateAsync(oldActivite);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    getResponse.Errors.Add("Activity can't be update.");

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
