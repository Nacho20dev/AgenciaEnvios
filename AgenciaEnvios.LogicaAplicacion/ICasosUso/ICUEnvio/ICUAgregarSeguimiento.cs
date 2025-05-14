using AgenciaEnvios.DTOs.DTOs.DTOSeguimiento;
using AgenciaEnvios.LogicaNegocio.Entidades;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUAgregarSeguimiento
    {
        void AgregarSeguimiento(DTOSeguimiento s,  int? logueadoId);



    }


}