
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {

        public Usuario FindByEmail(string email)
        {
            return _context.Empleados.Where(x => x.Email == email).SingleOrDefault();
        }

    }
}
