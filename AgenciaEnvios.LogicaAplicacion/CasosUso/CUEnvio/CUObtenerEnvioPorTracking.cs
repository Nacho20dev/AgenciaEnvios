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

   


    //Recibe un nro de trackin (string), llama al metodo EsGuidValido de la clase para chequear
    //que el formato es valido luego llama al metodo del repo findbyNroTracking que en caso de encontrarlo
    //mapea para devolverlo como un DTOAltaEnvio.
        public DTOAltaEnvio FindByNroTracking(string nroTracking)
    {
        

        if (!Envio.EsGuidValido(nroTracking))
            throw new GuidNoValidoEx("El número de tracking no tiene un formato válido.");

        var envio = _repositorioEnvio.FindByNroTracking(nroTracking);

        if (envio == null)
            throw new EnvioNoEncontradoEx();

        return MapperEnvio.EnvioToDTOEnvio(envio);
    }





}