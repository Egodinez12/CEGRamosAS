using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    
    public class BecaAlumnoController : ControllerBase
    {
        [HttpGet]
        [Route("Api/BecaAlumno/GetAll")]
        public IActionResult GetAll()
        {

            ML.Alumno alumno = new ML.Alumno();
            alumno.Beca = new ML.Beca();

            ML.Result result = BL.Alumno.GetAllBecaAlumnoInner();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }

        }


    }
}
