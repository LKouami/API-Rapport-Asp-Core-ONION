using Rapport.Domaine.Dtos;
using Rapport.Domaine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Service
{
    public interface IActiviteService
    {
        Task<BaseResponseDto<ActiviteDto>> CreateActivite(Activite activite);
        Task<BaseResponseDto<ActiviteDto>> GetActiviteById(Guid Id);
        Task<BaseResponseDto<IEnumerable<ActiviteDto>>> GetActivites();
        Task<BaseResponseDto<ActiviteDto>> UpdateActivite(Guid Id, Activite activite);
        Task<BaseResponseDto<ActiviteDto>> DeleteActivite(Guid Id);
     
    }
}
