namespace Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones
{
    public class InscripcionesGetDTO
    {
        public int InscripcionId { get; set; }
        public int EstudianteId { get; set; }
        public string NombreEstudiante { get; set; }
        public int ClaseId { get; set; }
        public string NombreClase { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
