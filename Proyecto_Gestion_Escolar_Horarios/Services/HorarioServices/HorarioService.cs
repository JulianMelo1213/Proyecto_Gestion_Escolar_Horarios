﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Horario;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.HorarioServices
{
    public class HorarioService : IHorarioService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public HorarioService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HorarioGetDTO>> GetAllAsync()
        {
            var horarios = await _context.Horarios
                .Include(h => h.Clase)
                .Include(h => h.Aula)
                .Include(h => h.Dia)
                .ToListAsync();

            return horarios.Select(h => new HorarioGetDTO
            {
                HorarioId = h.HorarioId,
                ClaseId = h.ClaseId,
                NombreClase = h.Clase.Nombre,
                AulaId = h.AulaId,
                NombreAula = h.Aula.Nombre,
                DiaId = h.DiaId,
                NombreDia = h.Dia.Nombre,
                HoraInicio = h.HoraInicio,
                HoraFin = h.HoraFin,
                FechaRegistro = h.FechaRegistro
            }).ToList();
        }

        public async Task<HorarioGetDTO> GetByIdAsync(int id)
        {
            var horario = await _context.Horarios
                .Include(h => h.Clase)
                .Include(h => h.Aula)
                .Include(h => h.Dia)
                .FirstOrDefaultAsync(h => h.HorarioId == id);

            if (horario == null)
            {
                return null;
            }

            return new HorarioGetDTO
            {
                HorarioId = horario.HorarioId,
                ClaseId = horario.ClaseId,
                NombreClase = horario.Clase.Nombre,
                AulaId = horario.AulaId,
                NombreAula = horario.Aula.Nombre,
                DiaId = horario.DiaId,
                NombreDia = horario.Dia.Nombre,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                FechaRegistro = horario.FechaRegistro
            };
        }

        public async Task<HorarioGetDTO> CreateAsync(HorarioInsertDTO horarioDto)
        {
            var horario = new Horario
            {
                ClaseId = horarioDto.ClaseId,
                AulaId = horarioDto.AulaId,
                DiaId = horarioDto.DiaId,
                HoraInicio = TimeOnly.Parse(horarioDto.HoraInicio),
                HoraFin = TimeOnly.Parse(horarioDto.HoraFin),
                FechaRegistro = DateTime.Now
            };

            if (await _context.Horarios.AnyAsync(h =>
                h.AulaId == horario.AulaId && h.DiaId == horario.DiaId &&
                ((h.HoraInicio < horario.HoraFin && h.HoraInicio >= horario.HoraInicio) ||
                (h.HoraFin > horario.HoraInicio && h.HoraFin <= horario.HoraFin))))
            {
                throw new ArgumentException("Ya existe un horario en el mismo aula, día y hora.");
            }

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();
            return _mapper.Map<HorarioGetDTO>(horario);
        }

        public async Task<HorarioGetDTO> UpdateAsync(int id, HorarioPutDTO horarioDto)
        {
            var existingHorario = await _context.Horarios.FindAsync(id);
            if (existingHorario == null)
            {
                throw new KeyNotFoundException();
            }

            if (await _context.Horarios.AnyAsync(h =>
                h.AulaId == horarioDto.AulaId && h.DiaId == horarioDto.DiaId &&
                ((h.HoraInicio < TimeOnly.Parse(horarioDto.HoraFin) && h.HoraInicio >= TimeOnly.Parse(horarioDto.HoraInicio)) ||
                (h.HoraFin > TimeOnly.Parse(horarioDto.HoraInicio) && h.HoraFin <= TimeOnly.Parse(horarioDto.HoraFin))) &&
                h.HorarioId != id))
            {
                throw new ArgumentException("Ya existe un horario en el mismo aula, día y hora.");
            }

            existingHorario.ClaseId = horarioDto.ClaseId;
            existingHorario.AulaId = horarioDto.AulaId;
            existingHorario.DiaId = horarioDto.DiaId;
            existingHorario.HoraInicio = TimeOnly.Parse(horarioDto.HoraInicio);
            existingHorario.HoraFin = TimeOnly.Parse(horarioDto.HoraFin);
            existingHorario.FechaRegistro = DateTime.Now;

            _context.Entry(existingHorario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<HorarioGetDTO>(existingHorario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return false;
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

