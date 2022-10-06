using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{   
    public class AlumnoController : ControllerBase
    {
        [HttpGet]
        [Route("Api/Alumno/GetAll")]
        public IActionResult GetAll()
        {
            //ML.Alumno alumno = new ML.Alumno();
            var result = BL.Alumno.GetAllLINQ();
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
        [Route("Api/Alumno/Add")]

        // POST api/Usuario/Add
        public ActionResult Post([FromBody] ML.Alumno alumno)
        {
            var result = BL.Alumno.AddLINQ(alumno);

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
        [Route("Api/Alumno/Update/{IdAlumno}")]

        // Update Usuario
        public ActionResult Put(int IdAlumno, [FromBody] ML.Alumno alumno) //Despliega un Text para ingresar el Id #Ingresar Id en Json
        {
            var result = BL.Alumno.UpdateLINQ(alumno);

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
        [Route("Api/Alumno/GetbyId/{idalumno}")]
        // GetById 
        public ActionResult GetById(int idalumno)
        {
            var result = BL.Alumno.GetByIdLINQ(idalumno);

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
        [Route("Api/Alumno/Delete/{id}")]

        // Delet Usuario 
        public ActionResult Delete(int id)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.IdAlumno = id;
            var result = BL.Alumno.DeleteLINQ(alumno);

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
