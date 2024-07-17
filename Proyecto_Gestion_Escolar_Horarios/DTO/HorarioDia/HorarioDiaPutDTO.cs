using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia
{
    public class HorarioDiaPutDTO
    {
        [Required]
        public int HorarioId { get; set; }

        [Required]
        public int DiaId { get; set; }
    }
}
