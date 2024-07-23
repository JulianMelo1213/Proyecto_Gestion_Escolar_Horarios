using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Clase
{
    public class ClasePutDTO
    {
        [Required(ErrorMessage = "El campo ClaseId es obligatorio.")]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo Nombre no puede exceder de 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        [StringLength(500, ErrorMessage = "El campo Descripcion no puede exceder de 500 caracteres.")]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = "El campo ProfesorId es obligatorio.")]
        public int ProfesorId { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
