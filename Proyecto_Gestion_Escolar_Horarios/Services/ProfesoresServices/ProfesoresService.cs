using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Profesores;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ProfesoresServices
{
    public class ProfesoresService : IProfesoresService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public ProfesoresService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesorGetDTO>> GetAllAsync()
        {
            var profesores = await _context.Profesores.ToListAsync();
            return _mapper.Map<List<ProfesorGetDTO>>(profesores);
        }

        public async Task<ProfesorGetDTO> GetByIdAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            return profesor == null ? null : _mapper.Map<ProfesorGetDTO>(profesor);
        }

        public async Task<ProfesorGetDTO> CreateAsync(ProfesorInsertDTO profesorDto)
        {
            var profesor = _mapper.Map<Profesores>(profesorDto);

            if (await _context.Profesores.AnyAsync(p => p.Email == profesor.Email))
            {
                throw new ArgumentException("Ya existe un profesor con ese email.");
            }

            profesor.FechaRegistro = DateTime.Now;

            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProfesorGetDTO>(profesor);
        }

        public async Task<ProfesorGetDTO> UpdateAsync(int id, ProfesorPutDTO profesorDto)
        {
            var existingProfesor = await _context.Profesores.FindAsync(id);
            if (existingProfesor == null)
            {
                throw new KeyNotFoundException();
            }

            if (await _context.Profesores.AnyAsync(p => p.Email == profesorDto.Email && p.ProfesorId != id))
            {
                throw new ArgumentException("Ya existe un profesor con ese email.");
            }

            _mapper.Map(profesorDto, existingProfesor);
            existingProfesor.FechaRegistro = DateTime.Now;

            _context.Entry(existingProfesor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<ProfesorGetDTO>(existingProfesor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return false;
            }

            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
