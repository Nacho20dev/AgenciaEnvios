using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class EmailInvalidoEx:Exception
    {
        public EmailInvalidoEx(string? message):base(message) { }
        
            
        
    }
}
