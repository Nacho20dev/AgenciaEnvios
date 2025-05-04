namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{
 
   public class ErrorDeFechaEx : Exception
    {
        public ErrorDeFechaEx()
        {
        }

        public ErrorDeFechaEx(string? message) : base(message)
        {
        }

        public ErrorDeFechaEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}