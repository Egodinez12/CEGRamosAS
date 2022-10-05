using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{    
    public class SemestreController : ControllerBase
    {
        [HttpGet]
        [Route("Api/Semestre/GetAll")]
        public IActionResult GetAll()
        {
            ML.Semestre semestre = new ML.Semestre();
            var result = BL.Semestre.GetAllLINQ();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [Route("Api/Semestre/Add")]
        public ActionResult Post([FromBody] ML.Semestre semestre)
        {
            var result = BL.Semestre.AddLINQ(semestre);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
