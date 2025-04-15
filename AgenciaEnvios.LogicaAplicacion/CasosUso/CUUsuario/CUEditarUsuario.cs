using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUEditarUsuario : ICUEditarUsuario
    {
        private IRepositorioUsuario _repoUsuario;

        public CUEditarUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        public void EditarUsuario(DTOUsuario dto)
        {
            try
            {
                Usuario u = MapperUsuario.DTOToUsuario(dto);
                u.Validar();
                _repoUsuario.Update(u);

            }
            catch (ApellidoVacioEx e)
            {
                throw e;
            }
            catch (NombreVacioEx e)
            {
               throw e;
            }

            catch (ContraseniaCortaEx e)
            {
                throw e;
            }

            catch (ContraseniaVaciaEx e) 
            {
                throw e;
            }

            catch (EmailInvalidoEx e) 
            {
                throw e;
            }

            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
