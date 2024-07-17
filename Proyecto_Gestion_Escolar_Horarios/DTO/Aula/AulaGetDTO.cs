namespace Proyecto_Gestion_Escolar_Horarios.DTO.Aula
{
    public class AulaGetDTO
    {
        public int AulaId { get; set; }
        public string Nombre { get; set; } = null!;
        public int Capacidad { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
