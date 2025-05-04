
namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{
    [Serializable]
    public class AgenciaNoEncontradaEx : Exception
    {
        public AgenciaNoEncontradaEx()
        {
        }

        public AgenciaNoEncontradaEx(string? message) : base(message)
        {
        }

        public AgenciaNoEncontradaEx(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}