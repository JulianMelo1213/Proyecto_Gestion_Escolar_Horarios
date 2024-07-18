using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Aula
{
    public class AulaInsertDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Range(1, 30)]
        public int Capacidad { get; set; }
    }
}
