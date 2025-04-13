using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using Microsoft.AspNetCore.Mvc;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private ICUAltaUsuario _cUAltaUsuario;
        

        //private ICULogin _cULogin;

        public UsuarioController(ICUAltaUsuario _CUAltaUsuario
      )
        {
            _cUAltaUsuario = _CUAltaUsuario;
           

            //_cULogin = _CULogin;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(new DTOAltaUsuario()); // Siempre mejor pasar el modelo vacío también acá
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

        



       


    }
}
