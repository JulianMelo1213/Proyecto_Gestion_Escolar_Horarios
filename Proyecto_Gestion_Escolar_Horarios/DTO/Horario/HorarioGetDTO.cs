namespace Proyecto_Gestion_Escolar_Horarios.DTO.Horario
{
    public class HorarioGetDTO
    {
        public int HorarioId { get; set; }
        public int ClaseId { get; set; }
        public string NombreClase { get; set; } = null!;
        public int AulaId { get; set; }
        public string NombreAula { get; set; } = null!;
        public int DiaId { get; set; }
        public string NombreDia { get; set; } = null!;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
