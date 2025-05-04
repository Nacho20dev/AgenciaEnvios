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
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System.Text.Json;
using AgenciaEnvios.LogicaAccesoDatos.Migrations;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUEditarUsuario : ICUEditarUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUEditarUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }

        public void EditarUsuario(DTOUsuario dto)
        {
            try
            {
                if (_repoUsuario.EsAdmin(dto.LogueadoId)  == true) 
                { 
                LogicaNegocio.Entidades.Usuario u = MapperUsuario.DTOToUsuario(dto);
                u.Validar();
                int idEntidad = _repoUsuario.Update(u);
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", idEntidad.ToString(), "Edit correcto: " + JsonSerializer.Serialize(u));


                _repoAuditoria.Auditar(aud);
                }
                else 
                {
                    Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: El usuario no es Administrador");
                    _repoAuditoria.Auditar(aud);
                    throw new Exception("El usuario no es Administrador");
                }

            }
            catch (ApellidoVacioEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (NombreVacioEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
           

            catch (EmailInvalidoEx e) 
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }

            catch (UsuarioNoValidoEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }


            catch (UsuarioNoAdministradorEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }

            

            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "EDIT", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }

        }

       

    }
}
