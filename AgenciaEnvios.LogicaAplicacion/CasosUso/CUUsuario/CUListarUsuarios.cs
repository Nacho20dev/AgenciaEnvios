using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
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
        private IRepositorioAuditoria _repoAuditoria;

        public CUListarUsuarios(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }


        //recibe el id de loguado ppara corroborar que es admin y en caso de que no lo sea lo audita y lanza la
        //excepción. En caso que sea admin, trae de la base la lista de usuarios. Audita que existan usuarios y en 
        //caso que si, mapea la lista para poder devolver la lista de dtos y audita el caso de exito.
        public List<DTOUsuario> ListarUsuarios(int logueadoId)
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
                    throw new UsuarioNoAdministradorEx("El usuario no tiene permisos para listar usuarios.");
                }

                List<Usuario> usuarios = _repoUsuario.FindAll();

                if (usuarios == null || usuarios.Count == 0)
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

                List<DTOUsuario> listDtoUsuario = MapperUsuario.FromListUsuarioToListDtoUsuario(usuarios);

                Auditoria auditoriaExitosa = new Auditoria(
                    logueadoId,
                    "LISTAR",
                    null,
                    "Listado correcto: " + usuarios.Count + " usuarios listados."
                );
                _repoAuditoria.Auditar(auditoriaExitosa);

                return listDtoUsuario;
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

        public List<DTOUsuario> ListarUsuarios()
        {


            List<Usuario> usuarios = _repoUsuario.FindAll();

            List<DTOUsuario> listDtoParaRetornar = MapperUsuario.FromListUsuarioToListDtoUsuario(usuarios);
            return listDtoParaRetornar;
        }
    }

}


