using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class UsuarioNoValidoEx:Exception
    {
        public UsuarioNoValidoEx(string? message) : base(message)
        {
            
        }
    }
}
