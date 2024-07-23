using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.InscripcionesServices
{
    public class InscripcionesService : IInscripcionesService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public InscripcionesService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InscripcionesGetDTO>> GetAllAsync()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Clase)
                .ToListAsync();
            var inscripcionesDto = inscripciones.Select(i => new InscripcionesGetDTO
            {
                InscripcionId = i.InscripcionId,
                EstudianteId = i.EstudianteId,
                NombreEstudiante = i.Estudiante.Nombre + " " + i.Estudiante.Apellido,
                ClaseId = i.ClaseId,
                NombreClase = i.Clase.Nombre,
                FechaRegistro = i.FechaRegistro
            }).ToList();
            return inscripcionesDto;
        }

        public async Task<InscripcionesGetDTO> GetByIdAsync(int id)
        {
            var inscripcion = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Clase)
                .FirstOrDefaultAsync(i => i.InscripcionId == id);
            return inscripcion == null ? null : new InscripcionesGetDTO
            {
                InscripcionId = inscripcion.InscripcionId,
                EstudianteId = inscripcion.EstudianteId,
                NombreEstudiante = inscripcion.Estudiante.Nombre + " " + inscripcion.Estudiante.Apellido,
                ClaseId = inscripcion.ClaseId,
                NombreClase = inscripcion.Clase.Nombre,
                FechaRegistro = inscripcion.FechaRegistro
            };
        }

        public async Task<InscripcionesGetDTO> CreateAsync(InscripcionesInsertDTO inscripcionesDto)
        {
            var existingInscripcion = await _context.Inscripciones
                .FirstOrDefaultAsync(i => i.EstudianteId == inscripcionesDto.EstudianteId && i.ClaseId == inscripcionesDto.ClaseId);

            if (existingInscripcion != null)
            {
                throw new ArgumentException("El estudiante ya está inscrito en esta clase.");
            }

            var inscripcion = _mapper.Map<Inscripciones>(inscripcionesDto);
            inscripcion.FechaRegistro = DateTime.Now;

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();
            return _mapper.Map<InscripcionesGetDTO>(inscripcion);
        }

        public async Task<InscripcionesGetDTO> UpdateAsync(int id, InscripcionesPutDTO inscripcionesDto)
        {
            var existingInscripcion = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Clase)
                .FirstOrDefaultAsync(i => i.InscripcionId == id);
            if (existingInscripcion == null)
            {
                throw new KeyNotFoundException();
            }

            _mapper.Map(inscripcionesDto, existingInscripcion);
            existingInscripcion.FechaRegistro = DateTime.Now;

            _context.Entry(existingInscripcion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<InscripcionesGetDTO>(existingInscripcion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                return false;
            }

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
