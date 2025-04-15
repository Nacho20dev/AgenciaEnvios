using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.WebApp.NewFolder;
using Microsoft.AspNetCore.Mvc;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ICUAltaUsuario _cuAltaUsuario;
        private readonly ICUListarUsuarios _cuListarUsuarios;
        private readonly ICUEliminarUsuario _cuEliminarUsuario;
        private readonly ICULogin _cuLogin;
        private readonly ICUObtenerUsuario _cuObtenerUsuario;
        private readonly ICUEditarUsuario _cuEditarUsuario;

        public UsuarioController(ICUAltaUsuario cuAltaUsuario, ICUListarUsuarios cuListarUsuarios,
            ICULogin cuLogin, ICUEliminarUsuario cuEliminarUsuario,ICUObtenerUsuario cuObtenerUsuario,
            ICUEditarUsuario cuEditarUsuario)
        {
            _cuAltaUsuario = cuAltaUsuario;
            _cuListarUsuarios = cuListarUsuarios;
            _cuEliminarUsuario = cuEliminarUsuario;
            _cuLogin = cuLogin;
            _cuObtenerUsuario = cuObtenerUsuario;
            _cuEditarUsuario = cuEditarUsuario;
        }

        //[LogueadoAuthorize]
        ////[FuncionarioAuthorize]
        ////[AdministradorAuthorize]
        public IActionResult Index()
        {
            ViewBag.mensaje = TempData["mensaje"]; 
            return View(_cuListarUsuarios.ListarUsuarios()); 
        }


        //[LogueadoAuthorize]
        //[AdministradorAuthorize]
        public IActionResult Create()
        {
            return View(new DTOAltaUsuario()); 
        }

        [HttpPost]
        public IActionResult Create(DTOAltaUsuario dto)
        {
            try
            {
                _cuAltaUsuario.AltaUsuario(dto);
                ViewBag.mensaje = "Alta correcta";
                return View(new DTOAltaUsuario());



            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(DTOUsuario dto)
        {
            try
            {
                
                DTOUsuario b = _cuLogin.VerificarDatosParaLogin(dto);
                HttpContext.Session.SetInt32("LogueadoId", (int)b.Id);
                HttpContext.Session.SetString("LogueadoRol", b.Rol);
                switch (b.Rol)
                {
                    case "Administrador":
                        return RedirectToAction("Index", "Usuario"); // Controlador y acción
                    case "Empleado":
                        return RedirectToAction("Index", "Usuario");
                    case "Cliente":
                        return RedirectToAction("Index", "Usuario");
                    default:
                        return RedirectToAction("Index", "Usuario");
                }

            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
                return View();
            }
            return View();
        }



        public IActionResult Delete(int id)
        {
            _cuEliminarUsuario.EliminarUsuario(id);
            return RedirectToAction("Index", "Usuario");
        }


        public IActionResult Edit(int id)
        {

            
            DTOUsuario model = _cuObtenerUsuario.ObtenerUsuario(id);
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(DTOUsuario dto)
        {
            try
            {
                dto.LogueadoId = HttpContext.Session.GetInt32("LogueadoId");
                _cuEditarUsuario.EditarUsuario(dto);
                return RedirectToAction("Index", "Usuario");
            }
            catch (NombreVacioEx e)
            {
                ViewBag.error = e.Message;
                return View(dto);
            }
            catch (ApellidoVacioEx e)
            {

                ViewBag.error = e.Message;
                return View(dto);
            }
            

            catch (EmailInvalidoEx e)
            {

                ViewBag.error = e.Message;
                return View(dto);
            }

            catch (Exception e) // <-- Excepciones no previstas
            {
                ViewBag.error = "Ocurrió un error inesperado. Por favor, intente nuevamente.";
                // Opcional: loggear el error real para diagnóstico
                Console.WriteLine(e); // O usar un logger si tenés
                return View(dto);
            }




        }

        
         


}
    
}






    
