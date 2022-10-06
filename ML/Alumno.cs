using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Fotografia { get; set; }
        public bool Sexo { get; set; }
        public int IdBeca { get; set; }   
        public List<object> AlumnoList { get; set; }

        public ML.Beca Beca { get; set; }

        


    }
}
