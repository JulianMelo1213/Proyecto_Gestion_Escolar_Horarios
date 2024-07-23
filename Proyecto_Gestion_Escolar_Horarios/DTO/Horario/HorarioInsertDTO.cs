using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Horario
{
    public class HorarioInsertDTO
    {
        [Required(ErrorMessage = "El campo ClaseId es obligatorio.")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El campo AulaId es obligatorio.")]
        public int AulaId { get; set; }

        [Required(ErrorMessage = "El campo DiaId es obligatorio.")]
        public int DiaId { get; set; }

        [Required(ErrorMessage = "El campo HoraInicio es obligatorio.")]
        public TimeOnly HoraInicio { get; set; }

        [Required(ErrorMessage = "El campo HoraFin es obligatorio.")]
        public TimeOnly HoraFin { get; set; }
    }
}
