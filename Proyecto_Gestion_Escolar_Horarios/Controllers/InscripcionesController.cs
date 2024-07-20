using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones;
using Proyecto_Gestion_Escolar_Horarios.Services.InscripcionesServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionesService _inscripcionesService;

        public InscripcionesController(IInscripcionesService inscripcionesService)
        {
            _inscripcionesService = inscripcionesService;
        }

        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionesGetDTO>>> GetInscripciones()
        {
            try
            {
                var inscripciones = await _inscripcionesService.GetAllAsync();
                return Ok(inscripciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionesGetDTO>> GetInscripcion(int id)
        {
            try
            {
                var inscripcion = await _inscripcionesService.GetByIdAsync(id);

                if (inscripcion == null)
                {
                    return NotFound("Inscripción no encontrada.");
                }

                return Ok(inscripcion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, InscripcionesPutDTO inscripcionesDto)
        {
            if (id != inscripcionesDto.InscripcionId)
            {
                return BadRequest("El ID de la inscripción en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedInscripcion = await _inscripcionesService.UpdateAsync(id, inscripcionesDto);
                return Ok(updatedInscripcion);
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

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult<InscripcionesGetDTO>> PostInscripcion(InscripcionesInsertDTO inscripcionesDto)
        {
            try
            {
                var createdInscripcion = await _inscripcionesService.CreateAsync(inscripcionesDto);
                return CreatedAtAction(nameof(GetInscripcion), new { id = createdInscripcion.InscripcionId }, createdInscripcion);
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

        // DELETE: api/Inscripciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            try
            {
                var deleted = await _inscripcionesService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Inscripción no encontrada.");
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
