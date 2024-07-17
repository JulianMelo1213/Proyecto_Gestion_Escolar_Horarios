using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class HorarioAsignatura
{
    public int HorarioAsignaturaId { get; set; }

    public int HorarioId { get; set; }

    public int DiaId { get; set; }

    public int ProfesorId { get; set; }

    public virtual Dia Dia { get; set; } = null!;

    public virtual Horario Horario { get; set; } = null!;

    public virtual Profesores Profesor { get; set; } = null!;
}
