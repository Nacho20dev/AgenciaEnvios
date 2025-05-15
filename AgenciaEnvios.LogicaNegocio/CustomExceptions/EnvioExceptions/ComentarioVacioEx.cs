using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions
{
    public class ComentarioVacioEx : Exception
    {

        public ComentarioVacioEx()
            : base("Debe ingresar un comentario.") { }

        public ComentarioVacioEx(string? message)
            : base(message) { }
    }

}
