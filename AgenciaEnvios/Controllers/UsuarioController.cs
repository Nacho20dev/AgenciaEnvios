using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.WebApp.NewFolder;
using Microsoft.AspNetCore.Mvc;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private ICUAltaUsuario _cUAltaUsuario;
        private ICUListarUsuarios _CuListarUsuarios;


        private ICUEliminarUsuario _CUEliminarUsuario;


        

        private ICULogin _cULogin;





        public UsuarioController(ICUAltaUsuario _CUAltaUsuario, ICUListarUsuarios CuListarUsuarios, ICULogin  _CULogin,
ICUEliminarUsuario _CUEliminarUsuario)
      
        {
            _cUAltaUsuario = _CUAltaUsuario;
            _CuListarUsuarios = CuListarUsuarios;

            _CUEliminarUsuario = _CUEliminarUsuario;

            _cULogin=_CULogin;






        }

        [LogueadoAuthorize]
        [FuncionarioAuthorize]
        [AdministradorAuthorize]
        public IActionResult Index()
        {
            ViewBag.mensaje = TempData["mensaje"]; 
            return View(_CuListarUsuarios.ListarUsuarios()); 
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
                _cUAltaUsuario.AltaUsuario(dto);
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
                
                DTOUsuario b = _cULogin.VerificarDatosParaLogin(dto);
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
            _CUEliminarUsuario.EliminarUsuario(id);
            return RedirectToAction("Index");
        }





    }
}
