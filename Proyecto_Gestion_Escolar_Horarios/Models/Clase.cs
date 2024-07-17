using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Clase
{
    public int ClaseId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int ProfesorId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();

    public virtual ICollection<Inscripciones> Inscripciones { get; set; } = new List<Inscripciones>();

    public virtual Profesores Profesor { get; set; } = null!;
}
