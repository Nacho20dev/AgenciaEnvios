using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class ApellidoVacioEx : Exception
    {
        public ApellidoVacioEx()
            : base("El apellido del usuario no puede estar vacío.") { }

        public ApellidoVacioEx(string? message)
            : base(message) { }

    }
}
