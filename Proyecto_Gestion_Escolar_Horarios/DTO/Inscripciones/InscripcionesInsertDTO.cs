using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones
{
    public class InscripcionesInsertDTO
    {
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "El ID de la clase es obligatorio.")]
        public int ClaseId { get; set; }
    }
}
