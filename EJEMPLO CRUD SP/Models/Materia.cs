using System;
using System.Collections.Generic;

namespace EJEMPLO_CRUD_SP.Models;

public partial class Materia
{
    public int IdMateria { get; set; }

    public string NombreMateria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Inscripcion> Inscripcions { get; set; } = new List<Inscripcion>();
}
