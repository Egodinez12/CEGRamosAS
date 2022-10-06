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
        public ActionResult Form(int? idAlumno)
        {
            IFormFile file = Request.Form.Files["IFImagen"];


            

            ML.Alumno alumno = new ML.Alumno();           
            ML.Result resultBeca = BL.Beca.GetAllLINQ();
            alumno.Beca = new ML.Beca();
            alumno.Beca.BecaList = resultBeca.Objects;

            if (idAlumno != null)
            {
                ML.Result result = BL.Alumno.GetByBeca(idAlumno.Value);
                if (result.Correct)
                {
                    alumno = ((ML.Alumno)result.Object);
                    alumno.Beca.BecaList = resultBeca.Objects;
                    

                    return View(alumno);

                }
                else
                {
                    ViewBag.Message = " No se realizo la consulta" + result.MessangeError;
                    return View("Modal");
                }
            }
            else
            {
                alumno.Beca.BecaList = resultBeca.Objects;
                return View(alumno);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {            
            ML.Result result = new ML.Result();
            ML.Result resultbeca = BL.Beca.GetAllLINQ();
            alumno.Beca = new ML.Beca();


            result = BL.Alumno.AsignarBecaUpdateAlumno(alumno);

            if (result.Correct)
            {
                ViewBag.Message = "El empleado se ha actualizado correctamente";
            }
            else
            {
                ViewBag.Message = "El empleado no se ha actualizado correctamente " + result.MessangeError;
            }
                                                  
            alumno.Beca.BecaList = resultbeca.Objects;
                         
            return PartialView("Modal");

        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
