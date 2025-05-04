namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    
   public class EmailNoRegistradoEx : Exception
    {
        public EmailNoRegistradoEx()
        {
        }

        public EmailNoRegistradoEx(string? message) : base(message)
        {
        }

        public EmailNoRegistradoEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}