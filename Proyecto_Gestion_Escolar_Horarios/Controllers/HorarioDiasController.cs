using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia;
using Proyecto_Gestion_Escolar_Horarios.Services.HorarioDiaServices;

namespace Proyecto_Gestion_Escolar_Horarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioDiaController : ControllerBase
    {
        private readonly IHorarioDiaService _horarioDiaService;

        public HorarioDiaController(IHorarioDiaService horarioDiaService)
        {
            _horarioDiaService = horarioDiaService;
        }

        // GET: api/HorarioDia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioDiaGetDTO>>> GetHorarioDias()
        {
            try
            {
                var horariosDias = await _horarioDiaService.GetAllAsync();
                return Ok(horariosDias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // GET: api/HorarioDia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HorarioDiaGetDTO>> GetHorarioDia(int id)
        {
            try
            {
                var horarioDia = await _horarioDiaService.GetByIdAsync(id);

                if (horarioDia == null)
                {
                    return NotFound("HorarioDia no encontrado.");
                }

                return Ok(horarioDia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // PUT: api/HorarioDia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorarioDia(int id, HorarioDiaPutDTO horarioDiaDto)
        {
            if (id != horarioDiaDto.HorarioDiaId)
            {
                return BadRequest("El ID del HorarioDia en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            try
            {
                var updatedHorarioDia = await _horarioDiaService.UpdateAsync(id, horarioDiaDto);
                return Ok(updatedHorarioDia);
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

        // POST: api/HorarioDia
        [HttpPost]
        public async Task<ActionResult<HorarioDiaGetDTO>> PostHorarioDia(HorarioDiaInsertDTO horarioDiaDto)
        {
            try
            {
                var createdHorarioDia = await _horarioDiaService.CreateAsync(horarioDiaDto);
                return CreatedAtAction(nameof(GetHorarioDia), new { id = createdHorarioDia.HorarioDiaId }, createdHorarioDia);
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

        // DELETE: api/HorarioDia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorarioDia(int id)
        {
            try
            {
                var deleted = await _horarioDiaService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound("HorarioDia no encontrado.");
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
