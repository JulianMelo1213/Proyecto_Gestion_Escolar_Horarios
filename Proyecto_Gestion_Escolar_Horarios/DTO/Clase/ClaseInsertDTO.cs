using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Clase
{
    public class ClaseInsertDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; } = null!;

        [Required]
        public int ProfesorId { get; set; }
    }
}
