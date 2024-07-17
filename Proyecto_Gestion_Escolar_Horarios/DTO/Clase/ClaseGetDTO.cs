namespace Proyecto_Gestion_Escolar_Horarios.DTO.Clase
{
    public class ClaseGetDTO
    {
        public int ClaseId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int ProfesorId { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
