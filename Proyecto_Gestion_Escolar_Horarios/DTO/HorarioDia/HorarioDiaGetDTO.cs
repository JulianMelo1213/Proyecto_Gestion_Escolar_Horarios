namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia
{
    public class HorarioDiaGetDTO
    {
        public int HorarioDiaId { get; set; }
        public int HorarioId { get; set; }
        public int DiaId { get; set; }
        public string NombreDia { get; set; } = null!;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public int ClaseId { get; set; }
        public int AulaId { get; set; }
    }
}
