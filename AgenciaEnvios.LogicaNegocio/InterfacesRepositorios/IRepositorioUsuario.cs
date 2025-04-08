using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios


{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario FindByEmail(string email);
    }
}