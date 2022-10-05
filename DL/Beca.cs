using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Beca
    {
        public Beca()
        {
            Alumnos = new HashSet<Alumno>();
        }

        public int IdBeca { get; set; }
        public string? NombreBeca { get; set; }
        public decimal? MontoMensual { get; set; }

        public virtual ICollection<Alumno> Alumnos { get; set; }
    }
}
