using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario
{
    public interface ICUEditarUsuario
    {
        void EditarUsuario(DTOUsuario dto);
    }
}
