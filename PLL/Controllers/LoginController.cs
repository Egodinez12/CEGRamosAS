using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Login(ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.GetAllByUsername(usuario.Username);
            if (result.Correct)
            {
                ML.Usuario resultUsuario = ((ML.Usuario)result.Object);
                if (resultUsuario.Clave == usuario.Clave)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Contraseña Incorrecta, Intente de nuevo";
                    return PartialView("modal");
                }
            }
            else
            {

                ViewBag.Message = "Email Incorrecta, Intente de nuevo";
            }
            return PartialView("modal");

        }
    }

}
