using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Alumno
    {
        public int IdAlumno { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPat { get; set; }
        public string? ApellidoMat { get; set; }
        public string? Fotografia { get; set; }
        public bool? Sexo { get; set; }
        public int? IdBeca { get; set; }

        public virtual Beca? IdBecaNavigation { get; set; }
    }
}
