using System;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaNegocio.Entidades;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUObtenerUsuario : ICUObtenerUsuario
    {
        private IRepositorioUsuario _repoUsuario;

        public CUObtenerUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        //Llama al metodo findbyid del repo para traer el usario a partir de el id que recibió por parámetro.
        //Luego lo mapea para poder devolver un DTO.
        public DTOUsuario ObtenerUsuario(int id)
        {
            Usuario b = _repoUsuario.FindById(id);

            return MapperUsuario.UsuarioToDTOUsuario(b);

        }
    }
}
