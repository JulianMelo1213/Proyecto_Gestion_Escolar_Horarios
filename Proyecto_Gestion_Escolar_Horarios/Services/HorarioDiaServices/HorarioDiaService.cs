﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioDiaServices
{
    public class HorarioDiaService : IHorarioDiaService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public HorarioDiaService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HorarioDiaGetDTO>> GetAllAsync()
        {
            var horariosDias = await _context.HorarioDias
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .ToListAsync();

            return horariosDias.Select(hd => new HorarioDiaGetDTO
            {
                HorarioDiaId = hd.HorarioDiaId,
                HorarioId = hd.HorarioId,
                DiaId = hd.DiaId,
                NombreDia = hd.Dia.Nombre,
                HoraInicio = hd.Horario.HoraInicio,
                HoraFin = hd.Horario.HoraFin,
                ClaseId = hd.Horario.ClaseId,
                AulaId = hd.Horario.AulaId
            }).ToList();
        }

        public async Task<HorarioDiaGetDTO> GetByIdAsync(int id)
        {
            var horarioDia = await _context.HorarioDias
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .FirstOrDefaultAsync(h => h.HorarioDiaId == id);

            if (horarioDia == null)
            {
                return null;
            }

            return new HorarioDiaGetDTO
            {
                HorarioDiaId = horarioDia.HorarioDiaId,
                HorarioId = horarioDia.HorarioId,
                DiaId = horarioDia.DiaId,
                NombreDia = horarioDia.Dia.Nombre,
                HoraInicio = horarioDia.Horario.HoraInicio,
                HoraFin = horarioDia.Horario.HoraFin,
                ClaseId = horarioDia.Horario.ClaseId,
                AulaId = horarioDia.Horario.AulaId
            };
        }

        public async Task<HorarioDiaGetDTO> CreateAsync(HorarioDiaInsertDTO horarioDiaDto)
        {
            var horarioDia = _mapper.Map<HorarioDia>(horarioDiaDto);

            // Validar que las relaciones existen
            if (!await _context.Horarios.AnyAsync(h => h.HorarioId == horarioDia.HorarioId))
            {
                throw new ArgumentException("El HorarioId proporcionado no existe.");
            }

            if (!await _context.Dias.AnyAsync(d => d.DiaId == horarioDia.DiaId))
            {
                throw new ArgumentException("El DiaId proporcionado no existe.");
            }

            _context.HorarioDias.Add(horarioDia);
            await _context.SaveChangesAsync();

            var createdHorarioDia = await _context.HorarioDias
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .FirstOrDefaultAsync(h => h.HorarioDiaId == horarioDia.HorarioDiaId);

            return new HorarioDiaGetDTO
            {
                HorarioDiaId = createdHorarioDia.HorarioDiaId,
                HorarioId = createdHorarioDia.HorarioId,
                DiaId = createdHorarioDia.DiaId,
                NombreDia = createdHorarioDia.Dia.Nombre,
                HoraInicio = createdHorarioDia.Horario.HoraInicio,
                HoraFin = createdHorarioDia.Horario.HoraFin,
                ClaseId = createdHorarioDia.Horario.ClaseId,
                AulaId = createdHorarioDia.Horario.AulaId
            };
        }

        public async Task<HorarioDiaGetDTO> UpdateAsync(int id, HorarioDiaPutDTO horarioDiaDto)
        {
            var existingHorarioDia = await _context.HorarioDias
                .Include(h => h.Dia)
                .Include(h => h.Horario)
                .FirstOrDefaultAsync(h => h.HorarioDiaId == id);

            if (existingHorarioDia == null)
            {
                throw new KeyNotFoundException("HorarioDia no encontrado.");
            }

            // Validar que las relaciones existen
            if (!await _context.Horarios.AnyAsync(h => h.HorarioId == horarioDiaDto.HorarioId))
            {
                throw new ArgumentException("El HorarioId proporcionado no existe.");
            }

            if (!await _context.Dias.AnyAsync(d => d.DiaId == horarioDiaDto.DiaId))
            {
                throw new ArgumentException("El DiaId proporcionado no existe.");
            }

            _mapper.Map(horarioDiaDto, existingHorarioDia);

            _context.Entry(existingHorarioDia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new HorarioDiaGetDTO
            {
                HorarioDiaId = existingHorarioDia.HorarioDiaId,
                HorarioId = existingHorarioDia.HorarioId,
                DiaId = existingHorarioDia.DiaId,
                NombreDia = existingHorarioDia.Dia.Nombre,
                HoraInicio = existingHorarioDia.Horario.HoraInicio,
                HoraFin = existingHorarioDia.Horario.HoraFin,
                ClaseId = existingHorarioDia.Horario.ClaseId,
                AulaId = existingHorarioDia.Horario.AulaId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var horarioDia = await _context.HorarioDias.FindAsync(id);
            if (horarioDia == null)
            {
                return false;
            }

            _context.HorarioDias.Remove(horarioDia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
