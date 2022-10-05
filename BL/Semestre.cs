using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Semestre
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from semestre in context.Semestres                                     
                                 select new
                                 {
                                     semestre.IdSemestre,
                                     semestre.NombreSemestre
                                 });
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Semestre semestre = new ML.Semestre();
                            semestre.IdSemestre = obj.IdSemestre;
                            semestre.NombreSemestre = obj.NombreSemestre;

                            result.Objects.Add(semestre);
                            result.Correct = true;

                        }
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.MessangeError = "Error al realizar la consulta";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.MessangeError = ex.Message;
                result.ex = ex;
            }
            return result;
        }

        public static ML.Result AddLINQ(ML.Semestre semestre)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    DL.Semestre semestreDL = new DL.Semestre();

                    semestreDL.NombreSemestre = semestre.NombreSemestre;
                    
                    context.Semestres.Add(semestreDL);
                    context.SaveChanges();
                    if (semestre != null)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.MessangeError = "No ha insertado ningun registro";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.MessangeError = ex.Message;
                result.ex = ex;
            }
            return result;
        }
    }
}
