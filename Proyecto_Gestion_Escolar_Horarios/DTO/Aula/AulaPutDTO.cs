using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Aula
{
    public class AulaPutDTO
    {
        [Required]
        public int AulaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;

        [Range(1, 30)]
        public int Capacidad { get; set; }

        public DateTime? FechaRegistro { get; set; } = DateTime.Now; 
    }

}
