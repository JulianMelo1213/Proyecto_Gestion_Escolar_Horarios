using AutoMapper;
using Proyecto_Gestion_Escolar_Horarios.DTO.Aula;
using Proyecto_Gestion_Escolar_Horarios.DTO.Clase;
using Proyecto_Gestion_Escolar_Horarios.DTO.Dia;
using Proyecto_Gestion_Escolar_Horarios.DTO.Estudiante;
using Proyecto_Gestion_Escolar_Horarios.DTO.Horario;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura;
using Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia;
using Proyecto_Gestion_Escolar_Horarios.DTO.Inscripciones;
using Proyecto_Gestion_Escolar_Horarios.DTO.Profesores;
using Proyecto_Gestion_Escolar_Horarios.Models;

namespace Proyecto_Gestion_Escolar_Horarios
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Aula
            CreateMap<AulaGetDTO, Aula>().ReverseMap();
            CreateMap<AulaInsertDTO, Aula>().ReverseMap();
            CreateMap<AulaPutDTO, Aula>().ReverseMap();

            // Mapeos de Clase
            CreateMap<ClaseGetDTO, Clase>().ReverseMap();
            CreateMap<ClaseInsertDTO, Clase>().ReverseMap();
            CreateMap<ClasePutDTO, Clase>().ReverseMap();

            // Mapeos de Dia
            CreateMap<DiaGetDTO, Dia>().ReverseMap();
            CreateMap<DiaInsertDTO, Dia>().ReverseMap();
            CreateMap<DiaPutDTO, Dia>().ReverseMap();

            // Mapeos de Estudiante
            CreateMap<EstudianteGetDTO, Estudiante>().ReverseMap();
            CreateMap<EstudianteInsertDTO, Estudiante>().ReverseMap();
            CreateMap<EstudiantePutDTO, Estudiante>().ReverseMap();

            // Mapeos de Horario
            CreateMap<HorarioGetDTO, Horario>().ReverseMap();
            CreateMap<HorarioInsertDTO, Horario>().ReverseMap();
            CreateMap<HorarioPutDTO, Horario>().ReverseMap();

            // Mapeos de HorarioAsignatura
            CreateMap<HorarioAsignaturaGetDTO, HorarioAsignatura>().ReverseMap();
            CreateMap<HorarioAsignaturaInsertDTO, HorarioAsignatura>().ReverseMap();
            CreateMap<HorarioAsignaturaPutDTO, HorarioAsignatura>().ReverseMap();

            // Mapeos de HorarioDia
            CreateMap<HorarioDiaGetDTO, HorarioDia>().ReverseMap();
            CreateMap<HorarioDiaInsertDTO, HorarioDia>().ReverseMap();
            CreateMap<HorarioDiaPutDTO, HorarioDia>().ReverseMap();

            // Mapeos de Inscripciones
            CreateMap<InscripcionesGetDTO, Inscripciones>().ReverseMap();
            CreateMap<InscripcionesInsertDTO, Inscripciones>().ReverseMap();
            CreateMap<InscripcionesPutDTO, Inscripciones>().ReverseMap();

            // Mapeos de Profesores
            CreateMap<ProfesorGetDTO, Profesores>().ReverseMap();
            CreateMap<ProfesorInsertDTO, Profesores>().ReverseMap();
            CreateMap<ProfesorPutDTO, Profesores>().ReverseMap();
        }
    }
}
