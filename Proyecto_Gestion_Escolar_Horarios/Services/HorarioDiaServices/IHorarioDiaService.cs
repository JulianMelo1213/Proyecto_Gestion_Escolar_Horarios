using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioDiaServices
{
    public interface IHorarioDiaService
    {
        Task<IEnumerable<HorarioDiaGetDTO>> GetAllAsync();
        Task<HorarioDiaGetDTO> GetByIdAsync(int id);
        Task<HorarioDiaGetDTO> CreateAsync(HorarioDiaInsertDTO horarioDiaDto);
        Task<HorarioDiaGetDTO> UpdateAsync(int id, HorarioDiaPutDTO horarioDiaDto);
        Task<bool> DeleteAsync(int id);
    }
}
