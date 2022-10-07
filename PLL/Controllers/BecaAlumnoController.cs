using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PLL.Controllers
{
    public class BecaAlumnoController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public BecaAlumnoController (IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult  GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();

            alumno.Beca = new ML.Beca();            
            ML.Result result = BL.Alumno.GetAllBecas(alumno.Beca.IdBeca);
            ML.Result resultBecas = BL.Beca.GetAllLINQ();
            alumno.Beca.BecaList = resultBecas.Objects;
            alumno.AlumnoList = result.Objects;

            return View(alumno);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Alumno alumno)
        {          
            //alumno.Beca = new ML.Beca();

            ML.Result result = BL.Alumno.GetAllBecas(alumno.Beca.IdBeca);
            ML.Result resultBecas = BL.Beca.GetAllLINQ();
            alumno.Beca.BecaList = resultBecas.Objects;
            alumno.AlumnoList = result.Objects;

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {

            ML.Alumno alumno = new ML.Alumno();
            if (IdEmpresa == null)
            {
                return View(alumno);
            }
            else
            {
                ML.Result result = BL.Alumno.GetByBeca(IdEmpresa.Value);
                if (result.Correct)
                {
                    alumno = ((ML.Alumno)result.Object);
                    return View(alumno);

                }
            }
            return View(alumno);
        }
        [HttpPost]

        public ActionResult Form(ML.Alumno alumno)
        {
            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFLogo"];

            //valido si traigo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                alumno.Fotografia = Convert.ToBase64String(ImagenBytes);
            }

            ML.Result result = new ML.Result();

            if (alumno.IdAlumno != 0)
            {

                result = BL.Alumno.AsignarBecaUpdateAlumno(alumno);
                if (result.Correct)
                {
                    ViewBag.Message = "Se Asigno de manera correcta ";
                    return PartialView("modal");
                }
                else
                {
                    ViewBag.Message = "No se ha asignado la beca " + result.MessangeError;
                    return PartialView("modal");
                }
            }
            else
            {
                return View(alumno);
            }
        }




        //[HttpGet]
        //public ActionResult Form(int? idAlumno)
        //{

        //    ML.Alumno alumno = new ML.Alumno();
        //    ML.Result result1 = BL.Beca.GetAllLINQ();
        //    alumno.Beca = new ML.Beca();
        //    alumno.Beca.BecaList = result1.Objects.ToList();

        //    if (idAlumno != null)
        //    {                
        //        ML.Result result = BL.Alumno.GetByBeca(idAlumno.Value);
        //        if (result.Correct)
        //        {

        //            alumno = ((ML.Alumno)result.Object);
        //            alumno.Beca = new ML.Beca();
        //            alumno.Beca.BecaList = result1.Objects.ToList();


        //            return View(alumno);

        //        }
        //        else
        //        {
        //            ViewBag.Message = " No se realizo la consulta" + result.MessangeError;
        //            return View("Modal");
        //        }



        //    }
        //    else
        //    {
        //        alumno.Beca.BecaList = result1.Objects.ToList();
        //        return PartialView(alumno);
        //    }

        //}

        //[HttpPost]
        //public ActionResult Form(ML.Alumno alumno)
        //{

        //    IFormFile file = Request.Form.Files["IFFoto"];

        //    //valido si traigo imagen
        //    if (file != null)
        //    {
        //        //llamar al metodo que convierte a bytes la imagen
        //        byte[] ImagenBytes = ConvertToBytes(file);
        //        //convierto a base 64 la imagen y la guardo en mi objeto materia
        //        alumno.Fotografia = Convert.ToBase64String(ImagenBytes);
        //    }
        //    ML.Result result = new ML.Result();
        //    ML.Result resultbeca = BL.Beca.GetAllLINQ();
        //    alumno.Beca = new ML.Beca();
        //    result = BL.Alumno.AsignarBecaUpdateAlumno(alumno);

        //    if (result.Correct)
        //    {
        //        ViewBag.Message = "El empleado se ha actualizado correctamente";
        //        return PartialView("Modal");
        //    }
        //    else
        //    {
        //        ViewBag.Message = "El empleado no se ha actualizado correctamente " + result.MessangeError;
        //        return PartialView("Modal");
        //    }

        //    alumno.Beca.BecaList = resultbeca.Objects;

        //    return View(alumno);

        //}

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
