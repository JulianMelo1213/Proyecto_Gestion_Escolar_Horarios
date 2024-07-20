using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones;

namespace Proyecto_Gestion_Escolar_Horarios.Services.InscripcionesServices
{
    public interface IInscripcionesService
    {
        Task<IEnumerable<InscripcionesGetDTO>> GetAllAsync();
        Task<InscripcionesGetDTO> GetByIdAsync(int id);
        Task<InscripcionesGetDTO> CreateAsync(InscripcionesInsertDTO inscripcionesDto);
        Task<InscripcionesGetDTO> UpdateAsync(int id, InscripcionesPutDTO inscripcionesDto);
        Task<bool> DeleteAsync(int id);
    }
}
