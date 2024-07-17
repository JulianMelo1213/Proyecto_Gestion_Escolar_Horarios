using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura
{
    public class HorarioAsignaturaInsertDTO
    {
        [Required]
        public int HorarioId { get; set; }

        [Required]
        public int DiaId { get; set; }

        [Required]
        public int ProfesorId { get; set; }
    }
}
