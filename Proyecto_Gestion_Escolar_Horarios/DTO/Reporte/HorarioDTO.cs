namespace Proyecto_Gestion_Escolar_Horarios.DTO.Reporte
{
    public class HorarioDTO
    {
        public string Clase { get; set; }
        public string Aula { get; set; }
        public string Dia { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
