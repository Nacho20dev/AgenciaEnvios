using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario:ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }


        public void AltaUsuario(DTOAltaUsuario dto)
        {

            Usuario buscado = _repoUsuario.FindByEmail(dto.Email);
            if (buscado != null)
            {
                throw new EmailYaExisteEx();
            }

            try
            {
                Usuario nuevo = MapperUsuario.DTOAltaToUsuario(dto);
                _repoUsuario.Add(nuevo);
            }
            catch (Exception e)
            {
                //Audito el problema
                throw e;
            }

        }

    }
}

