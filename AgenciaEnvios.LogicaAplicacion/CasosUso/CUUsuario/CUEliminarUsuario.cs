using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUEliminarUsuario : ICUEliminarUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUEliminarUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }

        public void EliminarUsuario(int id, int logueado)
        {
            try
            {
                if (_repoUsuario.EsAdmin(logueado) == true)
                {
                    // Primero busca el usuario por id
                    var usuario = _repoUsuario.FindById(id);

                    // Verifica si el usuario existe
                    if (usuario == null)
                    {
                        Auditoria audi = new Auditoria(logueado, "BAJA", null, "ERROR: Usuario no encontrado.");
                        _repoAuditoria.Auditar(audi);
                        throw new Exception("Usuario no encontrado.");
                    }

                    // Si el usuario existe, procede con la eliminación


                    int idEntidad = _repoUsuario.Remove(id);
                    Auditoria aud = new Auditoria(logueado, "BAJA", idEntidad.ToString(), "Baja exitosa: " + JsonSerializer.Serialize(usuario));
                }
                else
                {
                    Auditoria aud = new Auditoria(logueado, "BAJA", null, "ERROR: El usuario no es Administrador");
                    _repoAuditoria.Auditar(aud);
                    throw new Exception("El usuario no es Administrador");
                }

            }

            catch (Exception e)
            {
                Auditoria aud = new Auditoria(logueado, "BAJA", null, "ERROR: " + e.Message);

                _repoAuditoria.Auditar(aud);
                throw e;


            }
        }


    }
}