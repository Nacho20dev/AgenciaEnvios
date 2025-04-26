using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class UsuarioNoAdministradorEx : Exception
    {
        public UsuarioNoAdministradorEx(string? message) : base(message)
        {

        }
    }
}
