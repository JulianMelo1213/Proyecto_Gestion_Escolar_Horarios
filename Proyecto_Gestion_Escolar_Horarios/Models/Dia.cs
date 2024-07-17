using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Dia
{
    public int DiaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<HorarioAsignatura> HorarioAsignaturas { get; set; } = new List<HorarioAsignatura>();

    public virtual ICollection<HorarioDia> HorarioDia { get; set; } = new List<HorarioDia>();

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();
}
