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



        //Llama al filtro LoguadoAuthorize que chequea que esté logueado para poder acceder al controler.
        //Crea un viemodel para cargar las agencias en la vista. las trae  con el metodo del cu
        //obtenerAgencias. va cargando para cada una el nombre y el id en un SelectListItem el cual cargará en agenciaOrigen
        //del viewModel. hace lo mismo para agenciaDestino. Luego retorna el viewmodel con ambos SelectListItems cargados.
        [LogueadoAuthorize]
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

        //Recibe el viewmodel cargado con los datos de alta generados en el formulario. carga la variable de sesion
        //con el usuario logueado para la auditoría. LLama al metodo AltaEnvio del caso de uso y le pasa el DTO que
        //está dentro del viewmodel que es quien tiene el dto con los datos para el alta. Manda el mensaje "Alta Correcta"
        //en el caso de exito y redirige a la lista actualizada de envios.
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









        //Llama al filtro LoguadoAuthorize que chequea que esté logueado para poder acceder al controler.
        //Recibe un id que trajo del la vista anterior con el id del envio que quiere editar. Carga los
        //datos del envío en pantalla de un DTOAltaEnvio, a partir de llamar al método del caso de uso. Retorna 
        //la misma vista con estos datos que mostrará
        [LogueadoAuthorize]
         public IActionResult Edit(int id)
        {
            try
            {
               
                DTOAltaEnvio model = _cuObtenerEnvio.ObtenerEnvio(id);

                
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


        //Llama al filtro LoguadoAuthorize que chequea que esté logueado para poder acceder al controler.
        //carga en una lista de DTOAltaEnvio el DTO que recibe a través del metodo del cu ListarEnvios y
        //lo retorna a la misma vista.
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






        //recibe de la vista el id del envio a finalizar. trae del repo el usuario a partir de la
        //variable de sesión logueadoIDy le pasa al metodo del caso de uso FinalizarEnvio el id y el usuario.
        //Retorna a la misma vista con mensaje de exito. 
        public IActionResult Finalizar(int id)
        {

            int? vSession = HttpContext.Session.GetInt32("LogueadoId");
            Usuario u = _repositorioUsuario.FindById(vSession);
            _cuFinalizarEnvio.FinalizarEnvio(id, u);
            TempData["MensajeFinalizado"] = "Envío finalizado correctamente.";
            return RedirectToAction("ListarEnvios");
        }


        //recibe de la vista un id. Crea un vm que tendrá dentro el DTO de Envio
        //y el de seguimiento para poder dar alta a seguimineto dentro del envio.
        //carga dentro del DTOALtaEnvio el envio en el que se creará el nuevo seguimiento, }
        //no sin antes haberlo ido a buscar a la base. retorna la vista con el vm.
        [LogueadoAuthorize]
        public IActionResult CreateSeguimiento(int id)
        {

            AltaSeguimientoViewModel vm=new AltaSeguimientoViewModel();
            DTOAltaEnvio envio = _cuObtenerEnvio.ObtenerEnvio(id);
            
            vm.DTOAltaEnvio = envio;
            return View(vm);

        }

        // Recibe los datos de un nuevo seguimiento y el ID del envío.
        // Verifica si hay un usuario logueado en sesión y, si es así, agrega el seguimiento.
        // Redirige al listado de envíos si tiene éxito, o al login si no hay usuario.
        // Si ocurre un error (como comentario vacío u otro), vuelve a mostrar la vista.
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
