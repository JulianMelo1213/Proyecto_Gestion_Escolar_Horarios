using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Aula
{
    public class AulaInsertDTO
    {
        [Required(ErrorMessage = "El nombre del aula es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del aula no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Range(1, 30, ErrorMessage = "La capacidad del aula debe estar entre 1 y 30.")]
        public int Capacidad { get; set; }
    }
}
