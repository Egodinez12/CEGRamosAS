using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from materia in context.Materia
                                 join Semestre in context.Semestres on materia.IdSemestre equals Semestre.IdSemestre
                                 select new
                                 {
                                     materia.IdMateria,
                                     materia.NombreMateria,
                                     materia.Creditos,
                                     materia.IdSemestre,
                                     Semestre.NombreSemestre
                                 });
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.NombreMateria = obj.NombreMateria;
                            materia.Creditos = Convert.ToByte(obj.Creditos);

                            materia.Semestre = new ML.Semestre();
                            materia.IdSemestre = obj.IdSemestre.Value;
                            materia.Semestre.NombreSemestre = obj.NombreSemestre;

                            result.Objects.Add(materia);
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

        public static ML.Result GetByIdLINQ(int idmateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from materia in context.Materia
                                 join Semestre in context.Semestres on materia.IdSemestre equals Semestre.IdSemestre
                                 where materia.IdMateria == idmateria
                                 select new
                                 {
                                     materia.IdMateria,
                                     materia.NombreMateria,
                                     materia.Creditos,
                                     materia.IdSemestre,
                                     Semestre.NombreSemestre


                                 }).FirstOrDefault();

                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.IdMateria;
                        materia.NombreMateria = query.NombreMateria;
                        materia.Creditos = Convert.ToByte(query.Creditos);

                        materia.Semestre = new ML.Semestre();
                        materia.Semestre.IdSemestre = query.IdSemestre.Value;
                        materia.Semestre.NombreSemestre = query.NombreSemestre;

                        result.Object = materia;
                        result.Correct = true;


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

        public static ML.Result AddLINQ(ML.Materia materiaML)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    DL.Materium materium = new DL.Materium();

                    materium.NombreMateria = materiaML.NombreMateria;
                    materium.Creditos = materiaML.Creditos;
                    materium.IdSemestre = materiaML.IdSemestre;


                    context.Materia.Add(materium);
                    context.SaveChanges();
                    if (materiaML != null)
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

        public static ML.Result UpdateLINQ(ML.Materia materiMl)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from mat in context.Materia
                                 where mat.IdMateria == materiMl.IdMateria
                                 select mat).FirstOrDefault();



                    if (query != null)
                    {
                        query.NombreMateria = materiMl.NombreMateria;
                        query.Creditos = materiMl.Creditos;
                        query.IdSemestre = materiMl.IdSemestre;

                        context.SaveChanges();
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.MessangeError = "No ha modificado ningun registrado";
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

        public static ML.Result DeleteLINQ(ML.Materia materiMl)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from mat in context.Materia
                                 where mat.IdMateria == materiMl.IdMateria
                                 select mat).First();
                    if (query != null)
                    {
                        context.Materia.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.MessangeError = "No se eliminado  ningun Registro";

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
