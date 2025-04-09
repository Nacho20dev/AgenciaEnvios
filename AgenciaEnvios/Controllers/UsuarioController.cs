using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using Microsoft.AspNetCore.Mvc;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private ICUAltaUsuario _cUAltaUsuario;
        //private ICULogin _cULogin;
        
        public UsuarioController(ICUAltaUsuario _CUAltaUsuario, ICULogin _CULogin) 
        {
            _cUAltaUsuario = _CUAltaUsuario;
            _cULogin = _CULogin;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DTOAltaUsuario dto)
        {

            try
            {
                _cUAltaUsuario.AltaUsuario(dto);
                ViewBag.mensaje("Alta correcta");
            }
            catch (Exception ex) 
            {
                ViewBag.mensaje=ex.Message;
            }
            return View();
        }

    }
}
