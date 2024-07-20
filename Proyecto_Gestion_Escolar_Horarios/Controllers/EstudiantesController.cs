using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante;
using Proyecto_Gestion_Escolar_Horarios.Services.EstudianteServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteGetDTO>>> GetEstudiantes()
        {
            try
            {
                var estudiantes = await _estudianteService.GetAllAsync();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteGetDTO>> GetEstudiante(int id)
        {
            try
            {
                var estudiante = await _estudianteService.GetByIdAsync(id);

                if (estudiante == null)
                {
                    return NotFound("Estudiante no encontrado.");
                }

                return Ok(estudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Estudiantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, EstudiantePutDTO estudianteDto)
        {
            if (id != estudianteDto.EstudianteId)
            {
                return BadRequest("El ID del estudiante en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedEstudiante = await _estudianteService.UpdateAsync(id, estudianteDto);
                return Ok(updatedEstudiante);
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

        // POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult<EstudianteGetDTO>> PostEstudiante(EstudianteInsertDTO estudianteDto)
        {
            try
            {
                var createdEstudiante = await _estudianteService.CreateAsync(estudianteDto);
                return CreatedAtAction(nameof(GetEstudiante), new { id = createdEstudiante.EstudianteId }, createdEstudiante);
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

        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            try
            {
                var deleted = await _estudianteService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Estudiante no encontrado.");
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
