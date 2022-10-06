using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Materium
    {
        public int IdMateria { get; set; }
        public string? NombreMateria { get; set; }
        public byte? Creditos { get; set; }
        public int? IdSemestre { get; set; }

        public virtual Semestre? IdSemestreNavigation { get; set; }
    }
}
