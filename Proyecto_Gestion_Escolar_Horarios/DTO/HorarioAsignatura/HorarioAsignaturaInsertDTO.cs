﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioAsignatura
{
    public class HorarioAsignaturaInsertDTO
    {
        [Required(ErrorMessage = "El ID del horario es obligatorio.")]
        public int HorarioId { get; set; }

        [Required(ErrorMessage = "El ID del día es obligatorio.")]
        public int DiaId { get; set; }

        [Required(ErrorMessage = "El ID del profesor es obligatorio.")]
        public int ProfesorId { get; set; }

        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
    }
}
