namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{
   
  public class PesoInvalidoEx : Exception
    {
        public PesoInvalidoEx()
        {
        }

        public PesoInvalidoEx(string? message) : base(message)
        {
        }

        public PesoInvalidoEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}