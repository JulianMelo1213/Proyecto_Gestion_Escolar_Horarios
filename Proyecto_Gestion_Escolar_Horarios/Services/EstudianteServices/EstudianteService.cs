using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.EstudianteServices
{
    public class EstudianteService : IEstudianteService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public EstudianteService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstudianteGetDTO>> GetAllAsync()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            return _mapper.Map<List<EstudianteGetDTO>>(estudiantes);
        }

        public async Task<EstudianteGetDTO> GetByIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            return estudiante == null ? null : _mapper.Map<EstudianteGetDTO>(estudiante);
        }

        public async Task<EstudianteGetDTO> CreateAsync(EstudianteInsertDTO estudianteDto)
        {
            var estudiante = _mapper.Map<Estudiante>(estudianteDto);

            if (await _context.Estudiantes.AnyAsync(e => e.Email == estudiante.Email))
            {
                throw new ArgumentException("Ya existe un estudiante con ese email.");
            }

            estudiante.FechaRegistro = DateTime.Now;

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return _mapper.Map<EstudianteGetDTO>(estudiante);
        }

        public async Task<EstudianteGetDTO> UpdateAsync(int id, EstudiantePutDTO estudianteDto)
        {
            var existingEstudiante = await _context.Estudiantes.FindAsync(id);
            if (existingEstudiante == null)
            {
                throw new KeyNotFoundException();
            }

            if (await _context.Estudiantes.AnyAsync(e => e.Email == estudianteDto.Email && e.EstudianteId != id))
            {
                throw new ArgumentException("Ya existe un estudiante con ese email.");
            }

            _mapper.Map(estudianteDto, existingEstudiante);
            existingEstudiante.FechaRegistro = DateTime.Now;

            _context.Entry(existingEstudiante).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<EstudianteGetDTO>(existingEstudiante);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return false;
            }

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
