using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUListarUsuariosConNombreCompleto
    {

        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;
        private IRepositorioEnvio _repoEnvio;



        public DTOUsuario FindUsuarioNombreCompleto(int logueadoId)
        {
            try
            {
                if (!_repoUsuario.EsAdmin(logueadoId))
                {
                    Auditoria aud = new Auditoria(
                        logueadoId,
                        "LISTAR",
                        null,
                        "ERROR: El usuario no es Administrador"
                    );
                    _repoAuditoria.Auditar(aud);
                    throw new UsuarioNoAdministradorEx("El usuario no tiene permisos para listar envios.");
                }

                Usuario u = _repoUsuario.FindById(logueadoId);

                DTOUsuario dto = new DTOUsuario
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido
                };




                if (u == null)
                {
                    Auditoria aud = new Auditoria(
                        logueadoId,
                        "LISTAR",
                        null,
                        "ERROR: No se encontraron usuarios."
                    );
                    _repoAuditoria.Auditar(aud);
                    throw new ListadoUsuariosVacioEx("No hay usuarios registrados en el sistema.");
                }



                return dto;
            }
            catch (UsuarioNoAdministradorEx ex)
            {
                throw ex;
            }

            catch (Exception e)
            {
                Auditoria aud = new Auditoria(
                    logueadoId,
                    "LISTAR",
                    null,
                    "ERROR : " + e.Message
                );
                _repoAuditoria.Auditar(aud);
                throw new Exception("Error al intentar listar usuarios: " + e.Message);
            }
        }
    }
}
