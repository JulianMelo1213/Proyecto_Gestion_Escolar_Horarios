namespace Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante
{
    public class EstudianteGetDTO
    {
        public int EstudianteId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
