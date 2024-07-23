using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Clase;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ClaseServices
{
    public class ClaseService : IClaseService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public ClaseService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClaseGetDTO>> GetAllAsync()
        {
            var clases = await _context.Clases.ToListAsync();
            return _mapper.Map<List<ClaseGetDTO>>(clases);
        }

        public async Task<ClaseGetDTO> GetByIdAsync(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            return clase == null ? null : _mapper.Map<ClaseGetDTO>(clase);
        }

        public async Task<ClaseGetDTO> CreateAsync(ClaseInsertDTO claseDto)
        {
            var clase = _mapper.Map<Clase>(claseDto);

            if (await _context.Clases.AnyAsync(c => c.Nombre == clase.Nombre))
            {
                throw new ArgumentException("Ya existe una clase con ese nombre.");
            }

            clase.FechaRegistro = DateTime.Now;

            _context.Clases.Add(clase);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClaseGetDTO>(clase);
        }

        public async Task<ClaseGetDTO> UpdateAsync(int id, ClasePutDTO claseDto)
        {
            var existingClase = await _context.Clases.FindAsync(id);
            if (existingClase == null)
            {
                throw new KeyNotFoundException();
            }

            if (await _context.Clases.AnyAsync(c => c.Nombre == claseDto.Nombre && c.ClaseId != id))
            {
                throw new ArgumentException("Ya existe una clase con ese nombre.");
            }

            _mapper.Map(claseDto, existingClase);
            existingClase.FechaRegistro = DateTime.Now;

            _context.Entry(existingClase).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _mapper.Map<ClaseGetDTO>(existingClase);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase == null)
            {
                return false;
            }

            _context.Clases.Remove(clase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
