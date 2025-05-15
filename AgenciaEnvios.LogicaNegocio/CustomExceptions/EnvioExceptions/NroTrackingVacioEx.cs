namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{

    public class NroTrackingVacioEx : Exception
    {
        public NroTrackingVacioEx()
        {
        }

        public NroTrackingVacioEx(string? message) : base(message)
        {
        }

        public NroTrackingVacioEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}