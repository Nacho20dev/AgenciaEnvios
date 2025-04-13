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

        public UsuarioController(ICUAltaUsuario _CUAltaUsuario, ICUListarUsuarios CuListarUsuarios, ICULogin _CULogin, ICUEliminarUsuario _cUEliminarUsuario)
        {
            _cUAltaUsuario = _CUAltaUsuario;
            _CuListarUsuarios = CuListarUsuarios;
            _CUEliminarUsuario = _cUEliminarUsuario;
            _cULogin = _CULogin;
        }

        // Filtro para asegurarse que el usuario esté logueado y tenga el rol adecuado
        [LogueadoAuthorize] // Asegura que el usuario esté logueado
        [AdministradorAuthorize] // Solo los administradores pueden acceder a esta acción
        public IActionResult Index()
        {
            ViewBag.mensaje = TempData["mensaje"];
            return View(_CuListarUsuarios.ListarUsuarios()); // Lista a los usuarios
        }

        // Acción Create para crear un nuevo usuario, solo accesible para administradores
        [LogueadoAuthorize] // Asegura que el usuario esté logueado
        [AdministradorAuthorize] // Solo los administradores pueden acceder a esta acción
        public IActionResult Create()
        {
            return View(new DTOAltaUsuario()); // Carga la vista para dar de alta un usuario
        }

        // Acción para crear un nuevo usuario con el método POST, solo accesible para administradores
        [HttpPost]
        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Create(DTOAltaUsuario dto)
        {
            try
            {
                _cUAltaUsuario.AltaUsuario(dto); // Crea el nuevo usuario
                ViewBag.mensaje = "Alta correcta"; // Mensaje de éxito
                return View(new DTOAltaUsuario());
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message; // Muestra el mensaje de error si ocurre algo
                return View();
            }
        }

        // Login
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

                // Redirigir dependiendo del rol del usuario
                switch (b.Rol)
                {
                    case "Administrador":
                        // Si es Administrador, redirige al Create o Listado de Usuarios
                        return RedirectToAction("Create", "Usuario"); // Administrador puede acceder a Create
                    case "Funcionario":
                        // Si es Funcionario, lo rediriges a otro lugar o Dashboard
                        return RedirectToAction("Dashboard", "Home"); // O cualquier vista para el Funcionario
                    default:
                        // Para otros casos (si los roles cambian)
                        return RedirectToAction("Index", "Usuario"); // Podrías agregar una acción diferente
                }
            }
            catch (Exception ex)
            {
                ViewBag.mensaje = ex.Message; // Si hay un error, muestra el mensaje
                return View();
            }
        }

        // Eliminar un usuario, solo accesible para administradores
        [LogueadoAuthorize]
        [AdministradorAuthorize]
        public IActionResult Delete(int id)
        {
            _CUEliminarUsuario.EliminarUsuario(id); // Elimina al usuario por ID
            return RedirectToAction("Index", "Usuario"); // Redirige al listado de usuarios
        }

        // Acción para los funcionarios que no tienen acceso a ciertas páginas (ej. Create y Delete)
        [LogueadoAuthorize] // Asegura que el usuario esté logueado
        [FuncionarioAuthorize] // Solo funcionarios pueden acceder a esta acción
        public IActionResult Dashboard()
        {
            // Aquí puedes poner lo que el Funcionario puede ver, como su dashboard o vista general
            return View();
        }
    }

}
