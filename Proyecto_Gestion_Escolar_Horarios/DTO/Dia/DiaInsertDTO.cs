using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.Dia
{
    public class DiaInsertDTO
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
    }
}
