using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante;

namespace Proyecto_Gestion_Escolar_Horarios.Services.EstudianteServices
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteGetDTO>> GetAllAsync();
        Task<EstudianteGetDTO> GetByIdAsync(int id);
        Task<EstudianteGetDTO> CreateAsync(EstudianteInsertDTO estudianteDto);
        Task<EstudianteGetDTO> UpdateAsync(int id, EstudiantePutDTO estudianteDto);
        Task<bool> DeleteAsync(int id);
    }
}
