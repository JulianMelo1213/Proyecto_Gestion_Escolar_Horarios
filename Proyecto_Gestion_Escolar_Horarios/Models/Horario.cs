using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Horario
{
    public int HorarioId { get; set; }

    public int ClaseId { get; set; }

    public int AulaId { get; set; }

    public int DiaId { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Aula Aula { get; set; } = null!;

    public virtual Clase Clase { get; set; } = null!;

    public virtual Dia Dia { get; set; } = null!;

    public virtual ICollection<HorarioAsignatura> HorarioAsignaturas { get; set; } = new List<HorarioAsignatura>();

    public virtual ICollection<HorarioDia> HorarioDia { get; set; } = new List<HorarioDia>();
}
