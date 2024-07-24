using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Reporte;
using Proyecto_Gestion_Escolar_Horarios.Services.ReporteServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("utilizacion-aulas")]
        public async Task<ActionResult<IEnumerable<ReporteAulaDTO>>> GetUtilizacionAulas()
        {
            try
            {
                var reportes = await _reporteService.GetUtilizacionAulasAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("horarios-profesores")]
        public async Task<ActionResult<IEnumerable<ReporteProfesorDTO>>> GetHorarioProfesores()
        {
            try
            {
                var reportes = await _reporteService.GetHorarioPorProfesorAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("horarios-estudiantes")]
        public async Task<ActionResult<IEnumerable<ReporteEstudianteDTO>>> GetHorariosEstudiantes()
        {
            try
            {
                var reportes = await _reporteService.GetHorarioPorEstudianteAsync();
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}