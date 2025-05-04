namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{

    public class EnvioAMostrarVacioEx : Exception
    {
        public EnvioAMostrarVacioEx()
        {
        }

        public EnvioAMostrarVacioEx(string? message) : base(message)
        {
        }

        public EnvioAMostrarVacioEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}