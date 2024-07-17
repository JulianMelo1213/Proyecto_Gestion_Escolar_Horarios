namespace Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones
{
    public class InscripcionesGetDTO
    {
        public int InscripcionId { get; set; }
        public int EstudianteId { get; set; }
        public int ClaseId { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
