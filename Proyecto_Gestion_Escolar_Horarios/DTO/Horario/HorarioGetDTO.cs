namespace Proyecto_Gestion_Escolar_Horarios.DTO.Horario
{
    public class HorarioGetDTO
    {
        public int HorarioId { get; set; }
        public int ClaseId { get; set; }
        public int AulaId { get; set; }
        public int DiaId { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }

}
