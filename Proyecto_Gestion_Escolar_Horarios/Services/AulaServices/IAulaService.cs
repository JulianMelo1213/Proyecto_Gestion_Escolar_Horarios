using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Aula;
using Proyecto_Gestion_Escolar_Horarios.DTO;

namespace Proyecto_Gestion_Escolar_Horarios.Services.AulaServices
{
    public interface IAulaService
    {
        Task<IEnumerable<AulaGetDTO>> GetAllAsync();
        Task<AulaGetDTO> GetByIdAsync(int id);
        Task<AulaGetDTO> CreateAsync(AulaInsertDTO aulaDto);
        Task<AulaGetDTO> UpdateAsync(int id, AulaPutDTO aulaDto);
        Task<bool> DeleteAsync(int id);
    }
}
