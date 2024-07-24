using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Reporte;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.ReporteServices
{
    public class ReporteService : IReporteService
    {
        private readonly GestionEstudiantesContext _context;

        public ReporteService(GestionEstudiantesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReporteAulaDTO>> GetUtilizacionAulasAsync()
        {
            var horarios = await _context.Horarios
                .Include(h => h.Clase)
                .Include(h => h.Aula)
                .Include(h => h.Dia)
                .ToListAsync();

            var reportes = horarios
                .Where(h => h.Aula != null && h.Clase != null && h.Dia != null)
                .GroupBy(h => h.Aula.Nombre)
                .Select(g => new ReporteAulaDTO
                {
                    Aula = g.Key,
                    Cantidad = g.Count(),
                    Horarios = g.Select(h => new HorarioDTO
                    {
                        Clase = h.Clase.Nombre,
                        Dia = h.Dia.Nombre,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    }).ToList()
                }).ToList();

            return reportes;
        }

        public async Task<IEnumerable<ReporteProfesorDTO>> GetHorarioPorProfesorAsync()
        {
            var horariosAsignaturas = await _context.HorarioAsignaturas
                .Include(ha => ha.Profesor)
                .Include(ha => ha.Horario)
                    .ThenInclude(h => h.Clase)
                .Include(ha => ha.Horario)
                    .ThenInclude(h => h.Aula)
                .Include(ha => ha.Horario)
                    .ThenInclude(h => h.Dia)
                .ToListAsync();

            var reportes = horariosAsignaturas
                .Where(ha => ha.Profesor != null && ha.Horario != null && ha.Horario.Clase != null && ha.Horario.Aula != null && ha.Horario.Dia != null)
                .GroupBy(ha => $"{ha.Profesor.Nombre} {ha.Profesor.Apellido}")
                .Select(g => new ReporteProfesorDTO
                {
                    Profesor = g.Key,
                    Cantidad = g.Count(),
                    Horarios = g.Select(ha => new HorarioDTO
                    {
                        Clase = ha.Horario.Clase.Nombre,
                        Aula = ha.Horario.Aula.Nombre,
                        Dia = ha.Horario.Dia.Nombre,
                        HoraInicio = ha.Horario.HoraInicio,
                        HoraFin = ha.Horario.HoraFin
                    }).ToList()
                }).ToList();

            return reportes;
        }

        public async Task<IEnumerable<ReporteEstudianteDTO>> GetHorarioPorEstudianteAsync()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Clase)
                    .ThenInclude(c => c.Horarios)
                        .ThenInclude(h => h.Aula)
                .Include(i => i.Clase)
                    .ThenInclude(c => c.Horarios)
                        .ThenInclude(h => h.Dia)
                .ToListAsync();

            var reportes = inscripciones
                .Where(i => i.Estudiante != null && i.Clase != null && i.Clase.Horarios != null)
                .GroupBy(i => $"{i.Estudiante.Nombre} {i.Estudiante.Apellido}")
                .Select(g => new ReporteEstudianteDTO
                {
                    Estudiante = g.Key,
                    Cantidad = g.Count(),
                    Horarios = g.SelectMany(i => i.Clase.Horarios.Select(h => new HorarioDTO
                    {
                        Clase = i.Clase.Nombre,
                        Aula = h.Aula.Nombre,
                        Dia = h.Dia.Nombre,
                        HoraInicio = h.HoraInicio,
                        HoraFin = h.HoraFin
                    })).ToList()
                }).ToList();

            return reportes;
        }   
    }
}