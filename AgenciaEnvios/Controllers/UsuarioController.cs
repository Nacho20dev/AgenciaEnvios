using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.LoginExceptions;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.WebApp.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


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
            ICULogin cuLogin, ICUEliminarUsuario cuEliminarUsuario, ICUObtenerUsuario cuObtenerUsuario,
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
        ///Deberíamos cambiar el index para que sea accesible a 
        ///todos los usuarios logueados y que sea una vista general y el 
        /// que lo programado aqui sea un listar usuario del administrador


    

        [LogueadoAuthorize]
        public IActionResult Index()
        {
            string? nombreUsuario = HttpContext.Session.GetString("NombreUsuario");

            if (string.IsNullOrEmpty(nombreUsuario))
            {
                return RedirectToAction("Login", "Usuario");
            }

            ViewData["NombreUsuario"] = nombreUsuario;

            return View();
        }





        [LogueadoAuthorize]
        [AdministradorAuthorize]    
        public IActionResult ListarUsuarios()
        {
            try
            {

                List<DTOUsuario> usuarios = _cuListarUsuarios.ListarUsuarios();


                ViewBag.mensaje = TempData["mensaje"];


                return View(usuarios);
            }
            catch (UsuarioNoAdministradorEx ex)
            {

                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("ListarUsuarios");
            }
        }
        



        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Create()
        {
            return View(new DTOAltaUsuario());
        }

        [HttpPost]
        public IActionResult Create(DTOAltaUsuario dto)
        {
            try
            {
                // Recupera el ID del administrador logueado de la sesión
                int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");

                // Verifica si el ID del administrador se encontró en la sesión
                if (logueadoId.HasValue)
                {
                    // Asigna el ID del administrador al DTO
                    dto.LogueadoId = logueadoId.Value;

                    _cuAltaUsuario.AltaUsuario(dto);
                    TempData["AltaCorrecta"] = "Alta correcta";
                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                    // Si no se encuentra el ID en la sesión, algo anda mal con la autenticación
                    ViewBag.mensaje = "Error: No se pudo obtener el ID del administrador logueado.";
                    return View(); // O podrías redirigir a una página de error
                }
            }
            catch (NombreVacioEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (ApellidoVacioEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (ContraseniaVaciaEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (ContraseniaCortaEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (NoCumpleCaracteresEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (EmailInvalidoEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (UsuarioNoValidoEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (EmailYaExisteEx e)
            {
                ViewBag.mensaje = e.Message;
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message;
            }

            return View();
        }

        public IActionResult Login()
        {

            ViewBag.msj = HttpContext.Session.GetInt32("LogueadoId");
            return View();
        }


        [HttpPost]
        public IActionResult Login(DTOUsuario dto)
        {
            try
            {
                DTOUsuario b = _cuLogin.VerificarDatosParaLogin(dto);

                if (b == null)
                {
                    ViewBag.msj = "Credenciales inválidas.";
                    return View();
                }

                if (b.Id != null) 
                {
                    HttpContext.Session.SetInt32("LogueadoId", (int)b.Id);

                    if (!string.IsNullOrEmpty(b.Rol))
                    {
                        HttpContext.Session.SetString("LogueadoRol", b.Rol);
                    }
                    else
                    {
                        ViewBag.msj = "Error interno: El rol del usuario es nulo.";
                        return View();
                    }

                    if (!string.IsNullOrEmpty(b.Nombre))
                    {
                        HttpContext.Session.SetString("NombreUsuario", b.Nombre);
                    }
                    else
                    {
                        ViewBag.msj = "Error interno: El nombre del usuario es nulo.";
                        return View();
                    }

                    return RedirectToAction("Index"); 
                }
                else
                {
                    ViewBag.msj = "Error interno: El ID del usuario es nulo.";
                    return View();
                }
            }
            catch (EmailNoRegistradoEx e)
            {
                ViewBag.msj = e.Message;
                return View();
            }
            catch (DatosNoValidosEx e)
            {
                ViewBag.msj = e.Message;
                return View();
            }


            catch (ContraseniaVaciaEx e)
            {
                ViewBag.msj = e.Message;
                return View();
            }

            catch (Exception ex)
            {
                ViewBag.msj = ex.Message;
                return View();
            }
         
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }


        public IActionResult Remove(int id) 
        {
            
            int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");

            if (!logueadoId.HasValue)
            {
                
                return RedirectToAction("Login"); 
            }

         
            try
            {
                _cuEliminarUsuario.EliminarUsuario(id, logueadoId.Value); 
                TempData["Mensaje"] = "Usuario eliminado correctamente.";
                return RedirectToAction("ListarUsuarios");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrió un error al eliminar el usuario.";
                return View("Error");
            }
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
                dto.LogueadoId = (int)HttpContext.Session.GetInt32("LogueadoId");
                _cuEditarUsuario.EditarUsuario(dto);
                TempData["CambiosGuardados"] = "Cambios guardados correctamente.";
                return  RedirectToAction("ListarUsuarios", "Usuario");
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

            catch (Exception e) 
            {
                ViewBag.error = "Ocurrió un error inesperado. Por favor, intente nuevamente.";
               
                return View(dto);
            }




        }





    }

}






    
