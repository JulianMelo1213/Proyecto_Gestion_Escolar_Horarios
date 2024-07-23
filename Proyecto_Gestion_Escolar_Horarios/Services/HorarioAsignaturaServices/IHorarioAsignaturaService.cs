using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioAsignaturaServices
{
    public interface IHorarioAsignaturaService
    {
        Task<IEnumerable<HorarioAsignaturaGetDTO>> GetAllAsync();
        Task<HorarioAsignaturaGetDTO> GetByIdAsync(int id);
        Task<HorarioAsignaturaGetDTO> CreateAsync(HorarioAsignaturaInsertDTO dto);
        Task<HorarioAsignaturaGetDTO> UpdateAsync(int id, HorarioAsignaturaPutDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
