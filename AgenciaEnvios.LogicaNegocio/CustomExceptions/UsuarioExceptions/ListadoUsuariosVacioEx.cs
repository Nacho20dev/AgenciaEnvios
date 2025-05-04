
namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    
   public class ListadoUsuariosVacioEx : Exception
    {
        public ListadoUsuariosVacioEx()
        {
        }

        public ListadoUsuariosVacioEx(string? message) : base(message)
        {
        }

        public ListadoUsuariosVacioEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}