using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Aula
{
    public int AulaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Capacidad { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();
}
