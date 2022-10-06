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

            ML.Result result = BL.Alumno.GetAllBecaAlumnoInner();
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

        //public ActionResult Form(int? IdAlumno)
        //{
        //    ML.Alumno alumno = new ML.Alumno();
        //    if (IdAlumno != null)
        //    {
        //        ML.Result result = BL.Alumno.GetAllLINQ();

        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(_configuration["WebAPI"]);
        //            var responseTask = client.GetAsync("Api/Alumno/GetById/" + IdAlumno);
        //            responseTask.Wait();

        //            var resultAPI = responseTask.Result;
        //            if (resultAPI.IsSuccessStatusCode)
        //            {
        //                var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
        //                readTask.Wait();
        //                ML.Alumno resultItemList = new ML.Alumno();
        //                resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());
        //                result.Object = resultItemList;
        //            }

        //        }
        //        if (result.Correct)
        //        {
        //            alumno = ((ML.Alumno)result.Object);

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
        //        return View(alumno);
        //    }
        //}

        //[HttpPost]
        //public ActionResult Form(ML.Alumno alumno)
        //{
        //    IFormFile file = Request.Form.Files["IFImagen"];

        //    if (file != null)
        //    {

        //        byte[] ImagenBytes = ConvertToBytes(file);
        //        alumno.Fotografia = Convert.ToBase64String(ImagenBytes);
        //    }           
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(_configuration["WebAPI"]);

        //            //HTTP POST
        //            var postTask = client.PutAsJsonAsync<ML.Alumno>("Api/Alumno/Update/" + alumno.IdAlumno, alumno);
        //            postTask.Wait();

        //            var result = postTask.Result;

        //            if (result.IsSuccessStatusCode)
        //            {
        //                ViewBag.Message = "Se ha actualizado un Registro";
        //                return RedirectToAction("GetAll");
        //            }


        //        }
           
        //    return View("GetAll");


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
