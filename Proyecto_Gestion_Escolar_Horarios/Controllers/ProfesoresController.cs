using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Profesores;
using Proyecto_Gestion_Escolar_Horarios.Services.ProfesoresServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesoresService _profesoresService;

        public ProfesoresController(IProfesoresService profesoresService)
        {
            _profesoresService = profesoresService;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorGetDTO>>> GetProfesores()
        {
            try
            {
                var profesores = await _profesoresService.GetAllAsync();
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorGetDTO>> GetProfesor(int id)
        {
            try
            {
                var profesor = await _profesoresService.GetByIdAsync(id);

                if (profesor == null)
                {
                    return NotFound("Profesor no encontrado.");
                }

                return Ok(profesor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Profesores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(int id, ProfesorPutDTO profesorDto)
        {
            if (id != profesorDto.ProfesorId)
            {
                return BadRequest("El ID del profesor en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedProfesor = await _profesoresService.UpdateAsync(id, profesorDto);
                return Ok(updatedProfesor);
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

        // POST: api/Profesores
        [HttpPost]
        public async Task<ActionResult<ProfesorGetDTO>> PostProfesor(ProfesorInsertDTO profesorDto)
        {
            try
            {
                var createdProfesor = await _profesoresService.CreateAsync(profesorDto);
                return CreatedAtAction(nameof(GetProfesor), new { id = createdProfesor.ProfesorId }, createdProfesor);
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

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            try
            {
                var deleted = await _profesoresService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Profesor no encontrado.");
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
