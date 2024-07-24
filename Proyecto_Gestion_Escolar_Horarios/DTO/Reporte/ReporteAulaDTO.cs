namespace Proyecto_Gestion_Escolar_Horarios.DTO.Reporte
{
    public class ReporteAulaDTO
    {
        public string Aula { get; set; }
        public int Cantidad { get; set; }
        public List<HorarioDTO> Horarios { get; set; }
    }
}
