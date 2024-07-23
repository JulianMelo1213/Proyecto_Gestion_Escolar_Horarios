using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Horario;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioServices
{
    public interface IHorarioService
    {
        Task<IEnumerable<HorarioGetDTO>> GetAllAsync();
        Task<HorarioGetDTO> GetByIdAsync(int id);
        Task<HorarioGetDTO> CreateAsync(HorarioInsertDTO horarioDto);
        Task<HorarioGetDTO> UpdateAsync(int id, HorarioPutDTO horarioDto);
        Task<bool> DeleteAsync(int id);
    }
}
