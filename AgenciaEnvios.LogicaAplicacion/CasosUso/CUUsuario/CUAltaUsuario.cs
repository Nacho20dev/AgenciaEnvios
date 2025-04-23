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
using System.Text.Json;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario:ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
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
                Usuario nuevo = MapperUsuario.DTOToUsuario(dto);
                _repoUsuario.Add(nuevo);
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", nuevo.GetType().Name, idEntidad.ToString(), "Alta correcta" + JsonSerializer.Serialize(nuevo));

                _repoAuditoria.Auditar(aud);
            }
            catch (Exception e)
            {
                //Audito el problema
                throw e;
            }

        }

    }
}

