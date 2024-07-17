namespace Proyecto_Gestion_Escolar_Horarios.DTO.Profesores
{
    public class ProfesorGetDTO
    {
        public int ProfesorId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
    }
}
