using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioAsignaturaServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioAsignaturaController : ControllerBase
    {
        private readonly IHorarioAsignaturaService _horarioAsignaturaService;

        public HorarioAsignaturaController(IHorarioAsignaturaService horarioAsignaturaService)
        {
            _horarioAsignaturaService = horarioAsignaturaService;
        }

        // GET: api/HorarioAsignatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioAsignaturaGetDTO>>> GetHorarioAsignaturas()
        {
            try
            {
                var horariosAsignaturas = await _horarioAsignaturaService.GetAllAsync();
                return Ok(horariosAsignaturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/HorarioAsignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioAsignaturaGetDTO>> GetHorarioAsignatura(int id)
        {
            try
            {
                var horarioAsignatura = await _horarioAsignaturaService.GetByIdAsync(id);

                if (horarioAsignatura == null)
                {
                    return NotFound("HorarioAsignatura no encontrada.");
                }

                return Ok(horarioAsignatura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/HorarioAsignatura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioAsignatura(int id, HorarioAsignaturaPutDTO horarioAsignaturaDto)
        {
            if (id != horarioAsignaturaDto.HorarioAsignaturaId)
            {
                return BadRequest("El ID del HorarioAsignatura en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedHorarioAsignatura = await _horarioAsignaturaService.UpdateAsync(id, horarioAsignaturaDto);
                return Ok(updatedHorarioAsignatura);
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

        // POST: api/HorarioAsignatura
        [HttpPost]
        public async Task<ActionResult<HorarioAsignaturaGetDTO>> PostHorarioAsignatura(HorarioAsignaturaInsertDTO horarioAsignaturaDto)
        {
            try
            {
                var createdHorarioAsignatura = await _horarioAsignaturaService.CreateAsync(horarioAsignaturaDto);
                return CreatedAtAction(nameof(GetHorarioAsignatura), new { id = createdHorarioAsignatura.HorarioAsignaturaId }, createdHorarioAsignatura);
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

        // DELETE: api/HorarioAsignatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioAsignatura(int id)
        {
            try
            {
                var deleted = await _horarioAsignaturaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("HorarioAsignatura no encontrada.");
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
