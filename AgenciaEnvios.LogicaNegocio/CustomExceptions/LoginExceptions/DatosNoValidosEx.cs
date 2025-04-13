using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.LoginExceptions
{


    public class DatosNoValidosEx : Exception
    {
        public DatosNoValidosEx()
            : base("Datos ingrtesados no son correctos.") { }

        public DatosNoValidosEx(string? message)
            : base(message) { }
    }
}

