using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class EmailYaExisteEx : Exception
    {
        public EmailYaExisteEx()
           : base("Datos incorrectos.") { }

        public EmailYaExisteEx(string? message)
            : base(message) { }

    }
}
