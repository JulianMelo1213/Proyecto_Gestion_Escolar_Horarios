using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante
{
    public class EstudiantePutDTO
    {
        [Required(ErrorMessage = "El ID del estudiante es obligatorio.")]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del estudiante no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido del estudiante es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido del estudiante no puede exceder los 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El email del estudiante es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento del estudiante es obligatoria.")]
        public DateOnly FechaNacimiento { get; set; }

    }
}
