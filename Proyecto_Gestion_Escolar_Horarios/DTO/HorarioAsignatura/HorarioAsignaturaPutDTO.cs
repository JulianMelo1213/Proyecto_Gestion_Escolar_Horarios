using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura
{
    public class HorarioAsignaturaPutDTO
    {
        [Required(ErrorMessage = "El ID de la asignatura del horario es obligatorio.")]
        public int HorarioAsignaturaId { get; set; }

        [Required(ErrorMessage = "El ID del horario es obligatorio.")]
        public int HorarioId { get; set; }

        [Required(ErrorMessage = "El ID del día es obligatorio.")]
        public int DiaId { get; set; }

        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        public int ProfesorId { get; set; }
    }
}
