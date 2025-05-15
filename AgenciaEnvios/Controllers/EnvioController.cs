using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.DTOs.DTOs.DTOSeguimiento;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUAgencia;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using AgenciaEnvios.WebApp.Models.EnvioViewModel;
using AgenciaEnvios.WebApp.NewFolder;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AgenciaEnvios.WebApp.Controllers
{
    public class EnvioController : Controller
    {

        private ICUAltaEnvio _cuAltaEnvio;
        private ICUObtenerEnvio _cuObtenerEnvio;
        private ICUListarEnvios _cuListarEnvios;
        private ICUObtenerAgencia _cuObtenerAgencia;
        private ICUObtenerObjetoEnvio _cuObtenerObjetoEnvio;
        private ICUFinalizarEnvio _cuFinalizarEnvio;
        private ICUAgregarSeguimiento _cuAgregarSeguimiento;
        private IRepositorioEnvio _repositorioEnvio;
        private IRepositorioUsuario _repositorioUsuario;

        public EnvioController(ICUAltaEnvio cuEditarEnvio, ICUObtenerEnvio cuObtenerEnvio,
            ICUListarEnvios cuListarEnvios ,ICUObtenerAgencia cuObtenerAgencia, 
            ICUObtenerObjetoEnvio cuObtenerObjetoEnvio, IRepositorioEnvio repositorioEnvio,
            ICUFinalizarEnvio finalizarEnvio, IRepositorioUsuario repositorioUsuario, 
            ICUAgregarSeguimiento agregarSeguimiento
            )
        {
            _cuAltaEnvio = cuEditarEnvio;
            _cuObtenerEnvio = cuObtenerEnvio;
            _cuObtenerAgencia = cuObtenerAgencia;
            _cuListarEnvios = cuListarEnvios;
            _cuObtenerObjetoEnvio = cuObtenerObjetoEnvio;
            _repositorioEnvio = repositorioEnvio;
            _cuFinalizarEnvio = finalizarEnvio;
            _repositorioUsuario = repositorioUsuario;
            _cuAgregarSeguimiento = agregarSeguimiento;
        }


        [LogueadoAuthorize]
        //[AdministradorAuthorize]
        //[FuncionarioAuthorize]
        public IActionResult Create()
        {
            ViewData["Mensaje"] = HttpContext.Session.GetInt32("LogueadoId");

            AltaEnvioViewModel vm = new AltaEnvioViewModel();

            foreach (var agencia in _cuObtenerAgencia.ObtenerAgencias())
            {

                SelectListItem sitem = new SelectListItem();
                sitem.Text = agencia.Nombre;
                sitem.Value = agencia.Id.ToString();
                vm.AgenciasOrigen.Add(sitem);
            }

            foreach (var agencia in _cuObtenerAgencia.ObtenerAgencias())
            {

                SelectListItem sitem = new SelectListItem();
                sitem.Text = agencia.Nombre;
                sitem.Value = agencia.Id.ToString();
                vm.AgenciasDestino.Add(sitem);
            }
            return View(vm);
        }


        [HttpPost]
        public IActionResult Create(AltaEnvioViewModel vm)
        {
            try
            {
                int? variableSession = HttpContext.Session.GetInt32("LogueadoId");
                vm.Dto.IdLogueado = (int)variableSession;
                _cuAltaEnvio.AltaEnvio(vm.Dto);

               
                TempData["mensaje"] = "Alta correcta";
                return RedirectToAction("ListarEnvios");  
            }
            catch (UsuarioNoValidoEx e)
            {
                
                TempData["mensajeError"] = e.Message;
            }
            catch (AgenciaNoEncontradaEx e)
            {
                TempData["mensajeError"] = e.Message;
            }
            catch (PesoInvalidoEx e)
            {
                TempData["mensajeError"] = e.Message;
            }
            catch (EstadoInvalidoEx e)
            {
                TempData["mensajeError"] = e.Message;
            }
            catch (EmailNoRegistradoEx e)
            {
                TempData["mensajeError"] = e.Message;
            }
            catch (Exception ex)
            {
                TempData["mensajeError"] = ex.Message;
            }

          
            return View(vm);
        }











        [LogueadoAuthorize]
         public IActionResult Edit(int id)
        {
            try
            {
                // Obtener el envío a través del caso de uso
                DTOAltaEnvio model = _cuObtenerEnvio.ObtenerEnvio(id);

                // Devolvemos el modelo al View para editar
                return View(model);
            }
            catch (EnvioNoEncontradoEx e)
            {
                ViewBag.error = e.Message;
                return RedirectToAction("Index", "Envio");
            }
            catch (Exception e)
            {
                ViewBag.error = "Ocurrió un error inesperado. Por favor, intente nuevamente.";
                
                return RedirectToAction("Index", "Envio");
            }
        }


        [LogueadoAuthorize]
        public IActionResult ListarEnvios()
        {
            try
            {

                List<DTOAltaEnvio> envios = _cuListarEnvios.ListarEnvios();




                return View(envios);
            }
            catch (EnvioAMostrarVacioEx ex)
            {

                ViewBag.Mensaje = ex.Message;
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }







        public IActionResult Finalizar(int id)
        {

            int? vSession = HttpContext.Session.GetInt32("LogueadoId");
            Usuario u = _repositorioUsuario.FindById(vSession);
            _cuFinalizarEnvio.FinalizarEnvio(id, u);
            TempData["MensajeFinalizado"] = "Envío finalizado correctamente.";
            return RedirectToAction("ListarEnvios");
        }



        [LogueadoAuthorize]
        public IActionResult CreateSeguimiento(int id)
        {

            AltaSeguimientoViewModel vm=new AltaSeguimientoViewModel();
            DTOAltaEnvio envio = _cuObtenerEnvio.ObtenerEnvio(id);
            
            vm.DTOAltaEnvio = envio;
            return View(vm);

        }


        [HttpPost]
        public IActionResult CreateSeguimiento(AltaSeguimientoViewModel vm, int id)
        {
            try
            {

                vm.DTOSeguimiento.IdLogueado = HttpContext.Session.GetInt32("LogueadoId");
                if (vm.DTOSeguimiento.IdLogueado.HasValue)
                {
                    _cuAgregarSeguimiento.AgregarSeguimiento(vm.DTOSeguimiento, vm.DTOSeguimiento.IdLogueado);
                    TempData["AgregadoCorrectamente"] = "Agregado correctamente";
                    return RedirectToAction("ListarEnvios");
                }
                else 
                {
                    ViewBag.Mensaje = "No se pudo acceder.";
                    return RedirectToAction("Usuario","Login");
                }

                
            }
            
            catch(ComentarioVacioEx ex)
            {
                return View();

            }


            catch (Exception ex)
            {
                return View();
                   
           }

        }


       


    }

    }
