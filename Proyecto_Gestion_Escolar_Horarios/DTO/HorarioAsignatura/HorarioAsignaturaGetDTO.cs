namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura
{
    public class HorarioAsignaturaGetDTO
    {
        public int HorarioAsignaturaId { get; set; }
        public int HorarioId { get; set; }
        public int DiaId { get; set; }
        public int ProfesorId { get; set; }
        public string NombreDia { get; set; } = null!;
        public string NombreProfesor { get; set; } = null!;
        public string ApellidoProfesor { get; set; } = null!;
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
