using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Aula;
using Proyecto_Gestion_Escolar_Horarios.Services.AulaServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulasController : ControllerBase
    {
        private readonly IAulaService _aulaService;

        public AulasController(IAulaService aulaService)
        {
            _aulaService = aulaService;
        }

        // GET: api/Aulas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AulaGetDTO>>> GetAulas()
        {
            try
            {
                var aulas = await _aulaService.GetAllAsync();
                return Ok(aulas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Aulas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AulaGetDTO>> GetAula(int id)
        {
            try
            {
                var aula = await _aulaService.GetByIdAsync(id);

                if (aula == null)
                {
                    return NotFound("Aula no encontrada.");
                }

                return Ok(aula);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Aulas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAula(int id, AulaPutDTO aulaDto)
        {
            if (id != aulaDto.AulaId)
            {
                return BadRequest("El ID del aula en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedAula = await _aulaService.UpdateAsync(id, aulaDto);
                return Ok(updatedAula);
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

        // POST: api/Aulas
        [HttpPost]
        public async Task<ActionResult<AulaGetDTO>> PostAula(AulaInsertDTO aulaDto)
        {
            try
            {
                var createdAula = await _aulaService.CreateAsync(aulaDto);
                return CreatedAtAction(nameof(GetAula), new { id = createdAula.AulaId }, createdAula);
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

        // DELETE: api/Aulas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAula(int id)
        {
            try
            {
                var deleted = await _aulaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Aula no encontrada.");
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
