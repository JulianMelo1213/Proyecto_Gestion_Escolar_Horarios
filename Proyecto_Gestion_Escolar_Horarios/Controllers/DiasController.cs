using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Dia;
using Proyecto_Gestion_Escolar_Horarios.Services.DiaServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private readonly IDiaService _diaService;

        public DiaController(IDiaService diaService)
        {
            _diaService = diaService;
        }

        // GET: api/Dia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaGetDTO>>> GetDias()
        {
            try
            {
                var dias = await _diaService.GetAllAsync();
                return Ok(dias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Dia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaGetDTO>> GetDia(int id)
        {
            try
            {
                var dia = await _diaService.GetByIdAsync(id);

                if (dia == null)
                {
                    return NotFound("Día no encontrado.");
                }

                return Ok(dia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
