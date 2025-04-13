using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using Microsoft.AspNetCore.Mvc;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private ICUAltaUsuario _cUAltaUsuario;
        private ICUListarUsuarios _CuListarUsuarios;
        private ICUEliminarUsuario _CUEliminarUsuario;

        //private ICULogin _cULogin;

        public UsuarioController(ICUAltaUsuario _CUAltaUsuario, ICUListarUsuarios CuListarUsuarios, ICUEliminarUsuario CUEliminarUsuario

      )
        {
            _cUAltaUsuario = _CUAltaUsuario;
            _CuListarUsuarios = CuListarUsuarios;
            _CUEliminarUsuario = CUEliminarUsuario;


            //_cULogin = _CULogin;
        }


        public IActionResult Index()
        {
            ViewBag.mensaje = TempData["mensaje"]; 
            return View(_CuListarUsuarios.ListarUsuarios()); 
        }



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



        public IActionResult Delete(int id)
        {
            _CUEliminarUsuario.EliminarUsuario(id);
            return RedirectToAction("Index");
        }





    }
}
