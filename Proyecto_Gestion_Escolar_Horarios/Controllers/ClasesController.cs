using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Clase;
using Proyecto_Gestion_Escolar_Horarios.Services.ClaseServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasesController : ControllerBase
    {
        private readonly IClaseService _claseService;

        public ClasesController(IClaseService claseService)
        {
            _claseService = claseService;
        }

        // GET: api/Clases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaseGetDTO>>> GetClases()
        {
            try
            {
                var clases = await _claseService.GetAllAsync();
                return Ok(clases);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Clases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaseGetDTO>> GetClase(int id)
        {
            try
            {
                var clase = await _claseService.GetByIdAsync(id);

                if (clase == null)
                {
                    return NotFound("Clase no encontrada.");
                }

                return Ok(clase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Clases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClase(int id, ClasePutDTO claseDto)
        {
            if (id != claseDto.ClaseId)
            {
                return BadRequest("El ID de la clase en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedClase = await _claseService.UpdateAsync(id, claseDto);
                return Ok(updatedClase);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/Clases
        [HttpPost]
        public async Task<ActionResult<ClaseGetDTO>> PostClase(ClaseInsertDTO claseDto)
        {
            try
            {
                var createdClase = await _claseService.CreateAsync(claseDto);
                return CreatedAtAction(nameof(GetClase), new { id = createdClase.ClaseId }, createdClase);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/Clases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClase(int id)
        {
            try
            {
                var deleted = await _claseService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Clase no encontrada.");
                }

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
