using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario
{
    public interface ICUAltaUsuario
    {
        void AltaUsuario(DTOAltaUsuario dto);

    }
}
