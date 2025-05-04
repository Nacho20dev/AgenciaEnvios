namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{
    
   public class EstadoInvalidoEx : Exception
    {
        public EstadoInvalidoEx()
        {
        }

        public EstadoInvalidoEx(string? message) : base(message)
        {
        }

        public EstadoInvalidoEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}