using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Clase;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ClaseServices
{
    public interface IClaseService
    {
        Task<IEnumerable<ClaseGetDTO>> GetAllAsync();
        Task<ClaseGetDTO> GetByIdAsync(int id);
        Task<ClaseGetDTO> CreateAsync(ClaseInsertDTO claseDto);
        Task<ClaseGetDTO> UpdateAsync(int id, ClasePutDTO claseDto);
        Task<bool> DeleteAsync(int id);
    }
}
