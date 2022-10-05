using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public byte  Creditos { get; set; }
        public int IdSemestre { get; set; }

        public ML.Semestre Semestre { get; set; }
        public List<object> MateriaList { get; set; }

    }
}
