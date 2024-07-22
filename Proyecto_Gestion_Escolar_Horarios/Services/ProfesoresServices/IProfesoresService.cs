using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Profesores;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ProfesoresServices
{
    public interface IProfesoresService
    {
        Task<IEnumerable<ProfesorGetDTO>> GetAllAsync();
        Task<ProfesorGetDTO> GetByIdAsync(int id);
        Task<ProfesorGetDTO> CreateAsync(ProfesorInsertDTO profesorDto);
        Task<ProfesorGetDTO> UpdateAsync(int id, ProfesorPutDTO profesorDto);
        Task<bool> DeleteAsync(int id);
    }
}
