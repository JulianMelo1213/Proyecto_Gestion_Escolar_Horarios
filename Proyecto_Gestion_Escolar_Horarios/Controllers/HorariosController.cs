using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Horario;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly IHorarioService _horarioService;

        public HorariosController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioGetDTO>>> GetHorarios()
        {
            try
            {
                var horarios = await _horarioService.GetAllAsync();
                return Ok(horarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioGetDTO>> GetHorario(int id)
        {
            try
            {
                var horario = await _horarioService.GetByIdAsync(id);

                if (horario == null)
                {
                    return NotFound("Horario no encontrado.");
                }

                return Ok(horario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/Horarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, HorarioPutDTO horarioDto)
        {
            if (id != horarioDto.HorarioId)
            {
                return BadRequest("El ID del horario en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedHorario = await _horarioService.UpdateAsync(id, horarioDto);
                return Ok(updatedHorario);
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

        // POST: api/Horarios
        [HttpPost]
        public async Task<ActionResult<HorarioGetDTO>> PostHorario(HorarioInsertDTO horarioDto)
        {
            try
            {
                var createdHorario = await _horarioService.CreateAsync(horarioDto);
                return CreatedAtAction(nameof(GetHorario), new { id = createdHorario.HorarioId }, createdHorario);
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

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            try
            {
                var deleted = await _horarioService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("Horario no encontrado.");
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
