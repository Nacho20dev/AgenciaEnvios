using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;

public class CUObtenerEnvioPorTracking : ICUObtenerEnvioPorTracking
{

    private IRepositorioEnvio _repositorioEnvio;

   public CUObtenerEnvioPorTracking(IRepositorioEnvio repositorioEnvio)
    {
        _repositorioEnvio = repositorioEnvio;
    }

    public DTOAltaEnvio FindByNroTracking(string NroTracking)
    {
        DTOAltaEnvio dto = new DTOAltaEnvio();
        try
        {
            

            if (!string.IsNullOrEmpty(NroTracking))
            {
                if (Envio.EsGuidValido(NroTracking))
                {
                    Envio envio = _repositorioEnvio.FindByNroTracking(NroTracking);
                    if (envio != null)
                    {
                        dto = MapperEnvio.EnvioToDTOEnvio(envio);

                     
                    }

                    else
                    {
                        throw new EnvioNoEncontradoEx();
                    }
                }
                else
                {
                    throw new GuidNoValidoEx("No cumple formato");

                }
                
            }
             else
                {
                throw new NroTrackingVacioEx();
                }

           
        }


        catch (Exception ex)
        {

        }
        return dto;
    }
}