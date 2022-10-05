using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PLL.Controllers
{
    public class BecaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public BecaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Beca beca = new ML.Beca();
            ML.Result resultBeca = BL.Beca.GetAllLINQ();
            beca.BecaList = resultBeca.Objects;
            resultBeca.Objects = new List<object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("Api/Beca/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Beca resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(resultItem.ToString());
                        resultBeca.Objects.Add(resultItemList);
                    }
                }
                beca.BecaList = resultBeca.Objects;

            }
            return View(beca);

        }      
            [HttpGet]
            public ActionResult Form(int? IdBeca)
            {
                ML.Beca beca = new ML.Beca();
                if (IdBeca != null)
                {
                    ML.Result result = BL.Beca.GetAllLINQ();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);
                        var responseTask = client.GetAsync("Api/Beca/GetById/" + IdBeca);
                        responseTask.Wait();

                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Beca resultItemList = new ML.Beca();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Beca>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                        }

                    }
                    if (result.Correct)
                    {
                        beca = ((ML.Beca)result.Object);

                        return View(beca);

                    }
                    else
                    {
                        ViewBag.Message = " No se realizo la consulta" + result.MessangeError;
                        return View("Modal");
                    }
                }
                else
                {
                    return View(beca);
                }
            }
        

        [HttpPost]
        public ActionResult Form(ML.Beca beca)
        {

            if (beca.IdBeca == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);


                    var postTask = client.PostAsJsonAsync<ML.Beca>("Api/Beca/Add", beca);
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
                    var postTask = client.PutAsJsonAsync<ML.Beca>("Api/Beca/Update/" + beca.IdBeca, beca);
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
        public ActionResult Delete(ML.Beca beca)
        {

            ML.Result resultBeca = new ML.Result();
            int idBeca = beca.IdBeca;
            beca.IdBeca = idBeca;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5105/Api/");

                //HTTP POST
                var postTask = client.DeleteAsync("Beca/Delete/" + idBeca);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El usuario ha sido eliminada";
                }
                else
                {
                    ViewBag.Message = "El usuario no pudo ser eliminado" + resultBeca.MessangeError;
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
