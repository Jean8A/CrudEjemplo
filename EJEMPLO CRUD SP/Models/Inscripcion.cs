using System;
using System.Collections.Generic;

namespace EJEMPLO_CRUD_SP.Models;

public partial class Inscripcion
{
    public int IdInscripcion { get; set; }

    public int? IdEstudiante { get; set; }

    public int? IdMateria { get; set; }

    public DateOnly? FechaInscripcion { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }

    public virtual Materia? IdMateriaNavigation { get; set; }
}
