using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Gestion_Escolar_Horarios.DTO.Dia;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.Services.DiaServices
{
    public class DiaService : IDiaService
    {
        private readonly GestionEstudiantesContext _context;
        private readonly IMapper _mapper;

        public DiaService(GestionEstudiantesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiaGetDTO>> GetAllAsync()
        {
            var dias = await _context.Dias.ToListAsync();
            return _mapper.Map<List<DiaGetDTO>>(dias);
        }

        public async Task<DiaGetDTO> GetByIdAsync(int id)
        {
            var dia = await _context.Dias.FindAsync(id);
            return dia == null ? null : _mapper.Map<DiaGetDTO>(dia);
        }
    }
}
