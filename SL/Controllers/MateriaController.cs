
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    
    public class MateriaController : ControllerBase
    {
       // 
        [HttpGet]
        //[EnableCors]
        [Route("Api/Materia/GetAll")]
        public IActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            var result = BL.Materia.GetAll();
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
        [Route("Api/Materia/Add")]

        // POST api/Usuario/Add
        public ActionResult Post([FromBody] ML.Materia materia)
        {
            var result = BL.Materia.AddLINQ(materia);

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
        [Route("Api/Materia/Update/{IdMateria}")]

        // Update Usuario
        public ActionResult Put(int IdMateria, [FromBody] ML.Materia materia) //Despliega un Text para ingresar el Id #Ingresar Id en Json
        {
            var result = BL.Materia.UpdateLINQ(materia);

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
        [Route("Api/Materia/GetbyId/{idmateria}")]
        // GetById 
        public ActionResult GetById(int idmateria)
        {
            var result = BL.Materia.GetByIdLINQ(idmateria);

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
        [Route("Api/Materia/Delete/{id}")]

        // Delet Usuario 
        public ActionResult Delete(int id)
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = id;
            var result = BL.Materia.DeleteLINQ(materia);

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
