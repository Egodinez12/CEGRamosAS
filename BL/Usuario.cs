using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAllByUsername(string IdUsername)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CEGRamosAlfaSolucionesContext context = new DL.CEGRamosAlfaSolucionesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByIdUser '{IdUsername}' ").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Username = query.Username;
                        usuario.Clave = query.Clave;
                        usuario.IdUsuario = query.IdUsuario;


                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.MessangeError = " Ocurrio un error ";
                    }

                }
            }
            catch(Exception ex)
            {
                result.Correct=false;
                result.MessangeError=ex.Message;
                result.ex=ex;
            }
            return result;
        }
    }
}
