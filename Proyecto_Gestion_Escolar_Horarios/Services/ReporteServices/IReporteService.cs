using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto_Gestion_Escolar_Horarios.DTO.Reporte;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ReporteServices
{
    public interface IReporteService
    {
        Task<IEnumerable<ReporteAulaDTO>> GetUtilizacionAulasAsync();
        Task<IEnumerable<ReporteProfesorDTO>> GetHorarioPorProfesorAsync();
        Task<IEnumerable<ReporteEstudianteDTO>> GetHorarioPorEstudianteAsync();
    }
}