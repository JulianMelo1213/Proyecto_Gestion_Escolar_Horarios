namespace Proyecto_Gestion_Escolar_Horarios.DTO.Reporte
{
    public class ReporteProfesorDTO
    {
        public string Profesor { get; set; }
        public int Cantidad { get; set; }
        public List<HorarioDTO> Horarios { get; set; }
    }

}
