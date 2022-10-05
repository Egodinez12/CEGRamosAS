using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PLL.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public AlumnoController (IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Alumno alumno = new ML.Alumno();
            ML.Result resultAlumno = BL.Alumno.GetAllLINQ();
            alumno.AlumnoList = resultAlumno.Objects;
            resultAlumno.Objects = new List<object>();




            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("Api/Alumno/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync <ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                        resultAlumno.Objects.Add(resultItemList);
                    }
                }
                alumno.AlumnoList = resultAlumno.Objects;

            }
            return View(alumno);

        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (IdAlumno != null)
            {
                ML.Result result = BL.Alumno.GetAllLINQ();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);
                    var responseTask = client.GetAsync("Api/Alumno/GetById/" + IdAlumno);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Alumno resultItemList = new ML.Alumno();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;
                    }

                }
                if (result.Correct)
                {
                    alumno = ((ML.Alumno)result.Object);
                 
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
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {


            //obtengo la imagen
            IFormFile file = Request.Form.Files["IFImagen"];

            //valido si traigo imagen
            if (file != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(file);
                //convierto a base 64 la imagen y la guardo en mi objeto materia
                alumno.Fotografia = Convert.ToBase64String(ImagenBytes);
            }


            if (alumno.IdAlumno == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);


                    var postTask = client.PostAsJsonAsync<ML.Alumno>("Api/Alumno/Add", alumno);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                }


            }
            else
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ML.Alumno>("Api/Alumno/Update/" + alumno.IdAlumno, alumno);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha actualizado un Registro";
                        return RedirectToAction("GetAll");
                    }


                }

            }
            return View("GetAll");


        }
        [HttpGet]
        public ActionResult Delete(ML.Alumno alumno)
        {

            ML.Result resultAlumno = new ML.Result();
            int idAlumno = alumno.IdAlumno;
            alumno.IdAlumno = idAlumno;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5105/Api/");

                //HTTP POST
                var postTask = client.DeleteAsync("Alumno/Delete/" + idAlumno);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El usuario ha sido eliminada";
                }
                else
                {
                    ViewBag.Message = "El usuario no pudo ser eliminado" + resultAlumno.MessangeError;
                }
            }

            return PartialView("Modal");

        }

        public static byte[] ConvertToBytes(IFormFile fotografia)
        {

            using var fileStream = fotografia.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


    }
}
