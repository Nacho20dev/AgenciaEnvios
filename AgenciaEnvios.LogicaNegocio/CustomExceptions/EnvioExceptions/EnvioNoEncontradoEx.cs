namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{
   
    public class EnvioNoEncontradoEx : Exception
    {
        public EnvioNoEncontradoEx()
        {
        }

        public EnvioNoEncontradoEx(string? message) : base(message)
        {
        }

        public EnvioNoEncontradoEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}