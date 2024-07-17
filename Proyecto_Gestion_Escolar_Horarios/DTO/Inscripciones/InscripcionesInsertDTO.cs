using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones
{
    public class InscripcionesInsertDTO
    {
        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int ClaseId { get; set; }
    }
}
