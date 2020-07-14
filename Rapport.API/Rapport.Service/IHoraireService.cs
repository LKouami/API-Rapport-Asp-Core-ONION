using Rapport.Domaine.Dtos;
using Rapport.Domaine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rapport.Service
{
    public interface IHoraireService
    {
        Task<BaseResponseDto<HoraireDto>> CreateHoraire(Horaire horaire);
        Task<BaseResponseDto<HoraireDto>> GetHoraireById(Guid Id);
        Task<BaseResponseDto<IEnumerable<HoraireDto>>> GetHoraires();
        Task<BaseResponseDto<HoraireDto>> UpdateHoraire(Guid Id, Horaire horaire);
        Task<BaseResponseDto<HoraireDto>> DeleteHoraire(Guid Id);

    }
}
