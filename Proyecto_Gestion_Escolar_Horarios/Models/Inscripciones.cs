using System;
using System.Collections.Generic;

namespace Proyecto_Gestion_Escolar_Horarios.Models;

public partial class Inscripciones
{
    public int InscripcionId { get; set; }

    public int EstudianteId { get; set; }

    public int ClaseId { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Clase Clase { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
