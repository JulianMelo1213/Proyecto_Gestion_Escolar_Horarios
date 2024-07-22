using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Dia;

namespace Proyecto_Gestion_Escolar_Horarios.Services.DiaServices
{
    public interface IDiaService
    {
        Task<IEnumerable<DiaGetDTO>> GetAllAsync();
        Task<DiaGetDTO> GetByIdAsync(int id);
    }
}
