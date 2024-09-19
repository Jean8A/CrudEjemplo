using System;
using System.Collections.Generic;

namespace EJEMPLO_CRUD_SP.Models;

public partial class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
