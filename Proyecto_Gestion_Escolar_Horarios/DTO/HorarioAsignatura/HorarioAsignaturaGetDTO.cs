using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura
{
    public class HorarioAsignaturaGetDTO
    {
        public int HorarioAsignaturaId { get; set; }
        public int HorarioId { get; set; }
        public int DiaId { get; set; }
        public int ProfesorId { get; set; }

    }
}
