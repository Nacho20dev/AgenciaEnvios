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



    
        //Carga un index donde se toma de la variable de sesion el nombre de usuario, se chequea que no sea vacio
        //y se le envia a la vista que mostrará un mensaje de bienvenida. 
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




        //Se filtra por usuario logueado y que sea admin.
        //Se carga una lista de DTOUsuario a partur del metodo del caso de uso ListarUsuarios.
        //Se retorna la misma vista con este listado obtenido. Tira las excepciones correspondientes
        //en caso de no poder mostrar la lista 
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
        


        //valida que haya un usuario logueado y que sea admin.
        //retorna la misma vista con un nuevo DTOAltaUsuario que va permitir que se cree el formulario
        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Create()
        {
            return View(new DTOAltaUsuario());
        }



        //toma el id de logueado de la variable de sesion para cargarlo al dto.
        //valida si tiene algún valor ese loguadoid, en caso contrario lanza excepción.
        //En caso de tener vallor lo asigna y llama al metodo AltaUsuario del caso de uso.
        //MAnda un mensaje de exito y redirige al listado de usuario actualizado.
        [HttpPost]
        public IActionResult Create(DTOAltaUsuario dto)
        {
            try
            {

                int? logueadoId = HttpContext.Session.GetInt32("LogueadoId");


                if (logueadoId.HasValue)
                {

                    dto.LogueadoId = logueadoId.Value;

                    _cuAltaUsuario.AltaUsuario(dto);
                    TempData["AltaCorrecta"] = "Alta correcta";
                    return RedirectToAction("ListarUsuarios");
                }
                else
                {
                 
                    ViewBag.mensaje = "Error: No se pudo obtener el ID del administrador logueado.";
                    return View(); 
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


        //carga un dto con los datos que necesita al aprtir del dto que recibió por parametro que
        //solo tenía mail y contraseña, a la vez que verifica que email existe y contraseña se
        //corresponde con el. en caso de exito redirige a la vista del index. Sino lanza excepciones.
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

        //Hace un clear a las variables de sesión para limpiar el dato que vinculaba al
        //usuario logueado y deririge al index.
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }

        //carga una variable con el id del loguado y luego se chequea que no este vacio, osea que quien este intentando
        //hacer el remove esté loguado. En caso de exito llama al metodo del cu EliminarUsuario al que le pasa el id del
        //usuario que recibió por parametro(el que queria borrar).
        //Luego redirige nuevamente al listado de usuario con mensaje de exito y lista actualizada.
        //
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


        //recibe el id del usuario a editar, llama al metodo del cu ObtenerUsuario y le pasa el id por parametro. Esto 
        //es cargado en un dto para mostrar los datos del usuario y poder editarlos. 
        public IActionResult Edit(int id)
        {


            DTOUsuario model = _cuObtenerUsuario.ObtenerUsuario(id);
            return View(model);
        }


        //carga el id del logueado en el dto y luego llama al metodo del cu EditarUsuario pasando el dto por parametro.
        //redirige al listado de usuario actualizado. En caso de no cumplir lanza las excepciones correspondientes. 
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






    
