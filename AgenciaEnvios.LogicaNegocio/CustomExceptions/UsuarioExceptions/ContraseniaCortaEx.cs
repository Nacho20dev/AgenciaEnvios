using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions
{
    public class ContraseniaCortaEx:Exception
    {
        public ContraseniaCortaEx(string? message) : base(message)
        {
        }
    }
}
