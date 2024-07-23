using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Horario
{
    public class HorarioPutDTO
    {
        [Required(ErrorMessage = "El campo HorarioId es obligatorio.")]
        public int HorarioId { get; set; }

        [Required(ErrorMessage = "El campo ClaseId es obligatorio.")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El campo AulaId es obligatorio.")]
        public int AulaId { get; set; }

        [Required(ErrorMessage = "El campo DiaId es obligatorio.")]
        public int DiaId { get; set; }

        [Required(ErrorMessage = "El campo HoraInicio es obligatorio.")]
        public string HoraInicio { get; set; }  // Usar string en lugar de TimeOnly

        [Required(ErrorMessage = "El campo HoraFin es obligatorio.")]
        public string HoraFin { get; set; }  // Usar string en lugar de TimeOnly

        public DateTime? FechaRegistro { get; set; }
    }
}
