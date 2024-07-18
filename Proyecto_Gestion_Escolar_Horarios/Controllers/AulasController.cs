using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Gestion_Escolar_Horarios.DTO.Aula;
using Proyecto_Gestion_Escolar_Horarios.DTO;
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
            var aulas = await _aulaService.GetAllAsync();
            return Ok(aulas);
        }

        // GET: api/Aulas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AulaGetDTO>> GetAula(int id)
        {
            var aula = await _aulaService.GetByIdAsync(id);

            if (aula == null)
            {
                return NotFound();
            }

            return Ok(aula);
        }

        // PUT: api/Aulas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAula(int id, AulaPutDTO aulaDto)
        {
            if (id != aulaDto.AulaId)
            {
                return BadRequest();
            }

            try
            {
                var updatedAula = await _aulaService.UpdateAsync(id, aulaDto);
                return Ok(updatedAula);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
        }

        // DELETE: api/Aulas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAula(int id)
        {
            var deleted = await _aulaService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
