using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from beca in context.Becas
                                     
                                 select new
                                 {
                                    beca.IdBeca,
                                    beca.NombreBeca,
                                    beca.MontoMensual
                                 });
                    result.Objects = new List<object>();
                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Beca beca = new ML.Beca();
                           
                            beca.IdBeca = obj.IdBeca;
                            beca.NombreBeca = obj.NombreBeca;
                            beca.MontoMensual = Convert.ToDecimal(obj.MontoMensual);

                            result.Objects.Add(beca);
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

        public static ML.Result GetByIdLINQ(int IdBeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from beca in context.Becas                                     
                                 where beca.IdBeca == IdBeca
                                 select new
                                 {
                                     beca.IdBeca,
                                     beca.NombreBeca,
                                     beca.MontoMensual
                                 }).FirstOrDefault();

                   
                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        ML.Beca beca = new ML.Beca();

                        beca.IdBeca = query.IdBeca;
                        beca.NombreBeca = query.NombreBeca;
                        beca.MontoMensual = Convert.ToDecimal(query.MontoMensual);

                        result.Object = beca;
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

        public static ML.Result AddLINQ(ML.Beca becaMl)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    DL.Beca becaDL = new DL.Beca();

                    becaDL.NombreBeca = becaMl.NombreBeca;
                    becaDL.MontoMensual = becaMl.MontoMensual;

                    context.Becas.Add(becaDL);
                    context.SaveChanges();
                    if (becaMl != null)
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

        public static ML.Result UpdateLINQ(ML.Beca becaMl)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from bec in context.Becas
                                 where bec.IdBeca == becaMl.IdBeca
                                 select bec).SingleOrDefault();



                    if (query != null)
                    {
                        query.NombreBeca = becaMl.NombreBeca;
                        query.MontoMensual = becaMl.MontoMensual;

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

        public static ML.Result DeleteLINQ(ML.Beca becaMl)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = (from bec in context.Becas
                                 where bec.IdBeca == becaMl.IdBeca
                                 select bec).First();
                    if (query != null)
                    {
                        context.Becas.Remove(query);
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
