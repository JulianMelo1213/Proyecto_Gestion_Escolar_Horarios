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
            var horarios = await _context.Horarios.ToListAsync();
            return _mapper.Map<List<HorarioGetDTO>>(horarios);
        }

        public async Task<HorarioGetDTO> GetByIdAsync(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            return horario == null ? null : _mapper.Map<HorarioGetDTO>(horario);
        }

        public async Task<HorarioGetDTO> CreateAsync(HorarioInsertDTO horarioDto)
        {
            var horario = _mapper.Map<Horario>(horarioDto);

            if (await _context.Horarios.AnyAsync(h => h.ClaseId == horario.ClaseId && h.AulaId == horario.AulaId && h.DiaId == horario.DiaId && h.HoraInicio == horario.HoraInicio && h.HoraFin == horario.HoraFin))
            {
                throw new ArgumentException("Ya existe un horario con la misma clase, aula, día y horas de inicio y fin.");
            }

            horario.FechaRegistro = DateTime.Now;

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

            if (await _context.Horarios.AnyAsync(h => h.ClaseId == horarioDto.ClaseId && h.AulaId == horarioDto.AulaId && h.DiaId == horarioDto.DiaId && h.HoraInicio == horarioDto.HoraInicio && h.HoraFin == horarioDto.HoraFin && h.HorarioId != id))
            {
                throw new ArgumentException("Ya existe un horario con la misma clase, aula, día y horas de inicio y fin.");
            }

            _mapper.Map(horarioDto, existingHorario);
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