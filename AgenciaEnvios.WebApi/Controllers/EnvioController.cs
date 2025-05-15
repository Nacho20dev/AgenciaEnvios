using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using Microsoft.AspNetCore.Mvc;

namespace AgenciaEnvios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioController : ControllerBase
    {

        private ICUObtenerEnvioPorTracking _CuObtenerEnvioPorTracking;



        public EnvioController(ICUObtenerEnvioPorTracking CuObtenerEnvioPorTracking)
        {
            _CuObtenerEnvioPorTracking = CuObtenerEnvioPorTracking;
        }


        [HttpGet("{NroTracking}")]
        public ActionResult GetByNroTracking(string NroTracking)
        {
            try
            {
                var dto = _CuObtenerEnvioPorTracking.FindByNroTracking(NroTracking);
                return Ok(dto);
            }
          
            catch (GuidNoValidoEx)
            {
                return BadRequest("Formato inválido para número de tracking.");
            }
            catch (EnvioNoEncontradoEx)
            {
                return NotFound("No se encontró ningún envío con ese número de tracking.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error inesperado.");
            }
        }





    }

}



                
            



