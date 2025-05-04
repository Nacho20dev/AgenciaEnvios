using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
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
                return RedirectToAction("Index");
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
                _cuAltaUsuario.AltaUsuario(dto);
                ViewBag.mensaje = "Alta correcta";
                return View(new DTOAltaUsuario());



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
                HttpContext.Session.SetInt32("LogueadoId", (int)b.Id);
                HttpContext.Session.SetString("LogueadoRol", b.Rol);
                HttpContext.Session.SetString("NombreUsuario", b.Nombre);

                switch (b.Rol)
                {
                    case "Administrador":
                        return RedirectToAction("Index", "Usuario");
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



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Usuario");
        }
       

        public IActionResult Delete(int id)
        {
            //Necesitamos tener este logueadoId porque al no trabajar con DTO en el caso de uso necesitabamos pasarle 
            //de alguna forma el usuario logueado.
            // Busca el "claim" que representa el identificador único del usuario logueado.
            // Este claim suele tener el tipo "NameIdentifier" (por convención en ASP.NET).
            // Si el claim existe, accede a su valor (un string con el ID del usuario).
            // Si no existe (por seguridad o si el usuario no está logueado), se usa "0" como valor por defecto.
            // Convierte ese string en un entero, ya que la lógica probablemente necesita el ID como int.
            int logueadoId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");


            _cuEliminarUsuario.EliminarUsuario(id, logueadoId);
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
                dto.LogueadoId = (int)HttpContext.Session.GetInt32("LogueadoId");
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

            catch (Exception e) 
            {
                ViewBag.error = "Ocurrió un error inesperado. Por favor, intente nuevamente.";
               
                return View(dto);
            }




        }





    }

}






    
