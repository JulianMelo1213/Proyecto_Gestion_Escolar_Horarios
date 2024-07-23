using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Aula;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.AulaServices
{
    public class AulaService : IAulaService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public AulaService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AulaGetDTO>> GetAllAsync()
        {
            var aulas = await _context.Aulas.ToListAsync();
            return _mapper.Map<List<AulaGetDTO>>(aulas);
        }

        public async Task<AulaGetDTO> GetByIdAsync(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            return aula == null ? null : _mapper.Map<AulaGetDTO>(aula);
        }

        public async Task<AulaGetDTO> CreateAsync(AulaInsertDTO aulaDto)
        {
            var aula = _mapper.Map<Aula>(aulaDto);

            if (await _context.Aulas.AnyAsync(a => a.Nombre == aula.Nombre))
            {
                throw new ArgumentException("Ya existe un aula con ese nombre.");
            }

            aula.FechaRegistro = DateTime.Now;

            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();
            return _mapper.Map<AulaGetDTO>(aula);
        }

        public async Task<AulaGetDTO> UpdateAsync(int id, AulaPutDTO aulaDto)
        {
            var existingAula = await _context.Aulas.FindAsync(id);
            if (existingAula == null)
            {
                throw new KeyNotFoundException();
            }

            if (await _context.Aulas.AnyAsync(a => a.Nombre == aulaDto.Nombre && a.AulaId != id))
            {
                throw new ArgumentException("Ya existe un aula con ese nombre.");
            }

            _mapper.Map(aulaDto, existingAula);
            existingAula.FechaRegistro = DateTime.Now;

            _context.Entry(existingAula).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<AulaGetDTO>(existingAula);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null)
            {
                return false;
            }

            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
