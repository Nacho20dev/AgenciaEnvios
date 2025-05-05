using AgenciaEnvios.LogicaNegocio.Entidades;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUFinalizarEnvio
    {
        void FinalizarEnvio(int envioId, Usuario usuario);
    }
}