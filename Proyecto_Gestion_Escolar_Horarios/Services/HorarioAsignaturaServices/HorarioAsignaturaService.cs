using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioAsignaturaServices
{
    public class HorarioAsignaturaService : IHorarioAsignaturaService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public HorarioAsignaturaService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HorarioAsignaturaGetDTO>> GetAllAsync()
        {
            var horariosAsignaturas = await _context.HorarioAsignaturas
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .Include(h => h.Profesor)
                .ToListAsync();

            return horariosAsignaturas.Select(h => new HorarioAsignaturaGetDTO
            {
                HorarioAsignaturaId = h.HorarioAsignaturaId,
                HorarioId = h.HorarioId,
                DiaId = h.DiaId,
                ProfesorId = h.ProfesorId,
                NombreDia = h.Dia.Nombre,
                NombreProfesor = h.Profesor.Nombre,
                ApellidoProfesor = h.Profesor.Apellido,
                HoraInicio = h.Horario.HoraInicio,
                HoraFin = h.Horario.HoraFin
            }).ToList();
        }

        public async Task<HorarioAsignaturaGetDTO> GetByIdAsync(int id)
        {
            var horarioAsignatura = await _context.HorarioAsignaturas
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .Include(h => h.Profesor)
                .FirstOrDefaultAsync(h => h.HorarioAsignaturaId == id);

            if (horarioAsignatura == null)
            {
                return null;
            }

            return new HorarioAsignaturaGetDTO
            {
                HorarioAsignaturaId = horarioAsignatura.HorarioAsignaturaId,
                HorarioId = horarioAsignatura.HorarioId,
                DiaId = horarioAsignatura.DiaId,
                ProfesorId = horarioAsignatura.ProfesorId,
                NombreDia = horarioAsignatura.Dia.Nombre,
                NombreProfesor = horarioAsignatura.Profesor.Nombre,
                ApellidoProfesor = horarioAsignatura.Profesor.Apellido,
                HoraInicio = horarioAsignatura.Horario.HoraInicio,
                HoraFin = horarioAsignatura.Horario.HoraFin
            };
        }

        public async Task<HorarioAsignaturaGetDTO> CreateAsync(HorarioAsignaturaInsertDTO horarioAsignaturaDto)
        {
            var horarioAsignatura = _mapper.Map<HorarioAsignatura>(horarioAsignaturaDto);

            // Validar que las relaciones existen
            if (!await _context.Horarios.AnyAsync(h => h.HorarioId == horarioAsignatura.HorarioId))
            {
                throw new ArgumentException("El HorarioId proporcionado no existe.");
            }

            if (!await _context.Dias.AnyAsync(d => d.DiaId == horarioAsignatura.DiaId))
            {
                throw new ArgumentException("El DiaId proporcionado no existe.");
            }

            if (!await _context.Profesores.AnyAsync(p => p.ProfesorId == horarioAsignatura.ProfesorId))
            {
                throw new ArgumentException("El ProfesorId proporcionado no existe.");
            }

            var horario = await _context.Horarios.FindAsync(horarioAsignaturaDto.HorarioId);

            if (await _context.HorarioAsignaturas.AnyAsync(h => h.ProfesorId == horarioAsignaturaDto.ProfesorId && h.DiaId == horarioAsignaturaDto.DiaId &&
                ((h.Horario.HoraInicio >= horarioAsignaturaDto.HoraInicio && h.Horario.HoraInicio < horarioAsignaturaDto.HoraFin) ||
                (h.Horario.HoraFin > horarioAsignaturaDto.HoraInicio && h.Horario.HoraFin <= horarioAsignaturaDto.HoraFin))))
            {
                throw new ArgumentException("El profesor ya tiene una clase asignada en ese horario.");
            }

            // Nueva validación
            if (await _context.HorarioAsignaturas.AnyAsync(h => h.Horario.AulaId == horario.AulaId && h.DiaId == horarioAsignaturaDto.DiaId &&
                ((h.Horario.HoraInicio >= horario.HoraInicio && h.Horario.HoraInicio < horario.HoraFin) ||
                (h.Horario.HoraFin > horario.HoraInicio && h.Horario.HoraFin <= horario.HoraFin))))
            {
                throw new ArgumentException("Ya hay una clase asignada en el mismo aula, día y horario.");
            }

            _context.HorarioAsignaturas.Add(horarioAsignatura);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(horarioAsignatura.HorarioAsignaturaId);
        }

        public async Task<HorarioAsignaturaGetDTO> UpdateAsync(int id, HorarioAsignaturaPutDTO horarioAsignaturaDto)
        {
            var existingHorarioAsignatura = await _context.HorarioAsignaturas
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .Include(h => h.Profesor)
                .FirstOrDefaultAsync(h => h.HorarioAsignaturaId == id);

            if (existingHorarioAsignatura == null)
            {
                throw new KeyNotFoundException("HorarioAsignatura no encontrado.");
            }

            // Validar que las relaciones existen
            if (!await _context.Horarios.AnyAsync(h => h.HorarioId == horarioAsignaturaDto.HorarioId))
            {
                throw new ArgumentException("El HorarioId proporcionado no existe.");
            }

            if (!await _context.Dias.AnyAsync(d => d.DiaId == horarioAsignaturaDto.DiaId))
            {
                throw new ArgumentException("El DiaId proporcionado no existe.");
            }

            if (!await _context.Profesores.AnyAsync(p => p.ProfesorId == horarioAsignaturaDto.ProfesorId))
            {
                throw new ArgumentException("El ProfesorId proporcionado no existe.");
            }

            var horario = await _context.Horarios.FindAsync(horarioAsignaturaDto.HorarioId);

            if (await _context.HorarioAsignaturas.AnyAsync(h => h.ProfesorId == horarioAsignaturaDto.ProfesorId && h.DiaId == horarioAsignaturaDto.DiaId && h.HorarioAsignaturaId != id &&
                ((h.Horario.HoraInicio >= horarioAsignaturaDto.HoraInicio && h.Horario.HoraInicio < horarioAsignaturaDto.HoraFin) ||
                (h.Horario.HoraFin > horarioAsignaturaDto.HoraInicio && h.Horario.HoraFin <= horarioAsignaturaDto.HoraFin))))
            {
                throw new ArgumentException("El profesor ya tiene una clase asignada en ese horario.");
            }

            // Nueva validación
            if (await _context.HorarioAsignaturas.AnyAsync(h => h.Horario.AulaId == horario.AulaId && h.DiaId == horarioAsignaturaDto.DiaId && h.HorarioAsignaturaId != id &&
                ((h.Horario.HoraInicio >= horario.HoraInicio && h.Horario.HoraInicio < horario.HoraFin) ||
                (h.Horario.HoraFin > horario.HoraInicio && h.Horario.HoraFin <= horario.HoraFin))))
            {
                throw new ArgumentException("Ya hay una clase asignada en el mismo aula, día y horario.");
            }

            _mapper.Map(horarioAsignaturaDto, existingHorarioAsignatura);

            _context.Entry(existingHorarioAsignatura).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(existingHorarioAsignatura.HorarioAsignaturaId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var horarioAsignatura = await _context.HorarioAsignaturas.FindAsync(id);
            if (horarioAsignatura == null)
            {
                return false;
            }

            _context.HorarioAsignaturas.Remove(horarioAsignatura);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
