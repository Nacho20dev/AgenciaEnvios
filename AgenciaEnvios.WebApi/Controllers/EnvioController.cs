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

                DTOAltaEnvio dto = _CuObtenerEnvioPorTracking.FindByNroTracking(NroTracking);
                if (dto != null)
                {

                    return Ok(dto);
                }

                else
                {
                    return NotFound();
                }
            }

            catch (EnvioNoEncontradoEx)
            {
                return NotFound();
            }

            catch (GuidNoValidoEx)
            {
                return NotFound();
            }

            catch (NroTrackingVacioEx)
            {
                return BadRequest();
            }



            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }



    }

}



                
            



