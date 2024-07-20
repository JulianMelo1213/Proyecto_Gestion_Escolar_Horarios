using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Profesores
{
    public class ProfesorInsertDTO
    {
        [Required(ErrorMessage = "El nombre del profesor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del profesor no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido del profesor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido del profesor no puede exceder los 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El email del profesor es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
        public string Email { get; set; } = null!;
    }
}
