using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUListarUsuarios : ICUListarUsuarios
    {
        private IRepositorioUsuario _repoUsuario;

        public CUListarUsuarios(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public List<DTOUsuario> ListarUsuarios()
        {


            List<Usuario> usuarios = _repoUsuario.FindAll();

            List<DTOUsuario> listDtoParaRetornar = MapperUsuario.FromListUsuarioToListDtoUsuario(usuarios);
            return listDtoParaRetornar;
        }

    }
}


