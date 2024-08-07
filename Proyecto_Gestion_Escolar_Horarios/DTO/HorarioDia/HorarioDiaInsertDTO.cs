﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Gestion_Escolar_Horarios.DTO.HorarioDia
{
    public class HorarioDiaInsertDTO
    {
        [Required(ErrorMessage = "El ID del horario es obligatorio.")]
        public int HorarioId { get; set; }

        [Required(ErrorMessage = "El ID del día es obligatorio.")]
        public int DiaId { get; set; }
    }
}
