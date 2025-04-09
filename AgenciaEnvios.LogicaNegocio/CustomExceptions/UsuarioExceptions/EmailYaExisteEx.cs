using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class EmailYaExisteEx:Exception
    {
        public EmailYaExisteEx()
           : base("El nombre del usuario no puede estar vacío.") { }

        public EmailYaExisteEx(string? message)
            : base(message) { }

    }
}
