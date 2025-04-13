using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUEliminarUsuario : ICUEliminarUsuario
    {
        private IRepositorioUsuario _repoUsuario;

        public CUEliminarUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public void EliminarUsuario(int id)
        {
            // Primero busca el usuario por id
            var usuario = _repoUsuario.FindById(id);

            // Verifica si el usuario existe
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            // Si el usuario existe, procede con la eliminación
            _repoUsuario.Remove(id);
        }
    }


}
