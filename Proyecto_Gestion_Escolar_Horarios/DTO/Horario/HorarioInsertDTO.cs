using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Horario
{
    public class HorarioInsertDTO
    {
        [Required]
        public int ClaseId { get; set; }

        [Required]
        public int AulaId { get; set; }

        [Required]
        public int DiaId { get; set; }

        [Required]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        public TimeOnly HoraFin { get; set; }
    }
}
