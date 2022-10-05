using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class BecaController : ControllerBase
    {
        [HttpGet]
        //[EnableCors]
        [Route("Api/Beca/GetAll")]
        public IActionResult GetAll()
        {
            ML.Beca beca = new ML.Beca();
            var result = BL.Beca.GetAllLINQ();
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
        [Route("Api/Beca/Add")]

        // POST api/Usuario/Add
        public ActionResult Post([FromBody] ML.Beca beca)
        {
            var result = BL.Beca.AddLINQ(beca);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut] // si se ingresa un parametro siempre va dentro de {}, por lo general son los ID WHERE  
        [Route("Api/Beca/Update/{IdBeca}")]

        // Update Usuario
        public ActionResult Put(int IdBeca, [FromBody] ML.Beca beca) //Despliega un Text para ingresar el Id #Ingresar Id en Json
        {
            var result = BL.Beca.UpdateLINQ(beca);

            if (result.Correct)

            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Route("Api/Beca/GetbyId/{idbeca}")]
        // GetById 
        public ActionResult GetById(int idBeca)
        {

            var result = BL.Beca.GetByIdLINQ(idBeca);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("Api/Beca/Delete/{id}")]

        // Delet Usuario 
        public ActionResult Delete(int id)
        {
            ML.Beca beca = new ML.Beca();
            beca.IdBeca = id;
            var result = BL.Beca.DeleteLINQ(beca);

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
