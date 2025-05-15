using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using AgenciaEnvios.DTOs.DTOs.DTOSeguimiento;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUAgregarSeguimiento: ICUAgregarSeguimiento
    {
        private IRepositorioEnvio _repositorioEnvio;
        private IRepositorioAuditoria _repoAuditoria;
        private IRepositorioUsuario _repoUsuario;

        public CUAgregarSeguimiento(IRepositorioEnvio repoEnvio, IRepositorioAuditoria repoAuditoria, 
            IRepositorioUsuario repoUsuario)
        {
            _repositorioEnvio = repoEnvio;
            _repoAuditoria = repoAuditoria;
            _repoUsuario = repoUsuario;
        }

        public void AgregarSeguimiento(DTOSeguimiento s,  int? idLogueado)
        {
            try
            {
                Envio env = _repositorioEnvio.FindById(s.IdEnvio);

                Seguimiento seg= new Seguimiento();
                seg.Comentario = s.comentario;
                seg.Usuario = _repoUsuario.FindById(idLogueado);
                env.Seguimientos.Add(seg);
                _repositorioEnvio.Update(env);
                AuditarExito((int)idLogueado, (int)seg.Id, seg);

            }
            catch(ComentarioVacioEx ex) 
            { 
            AuditarError(idLogueado, "Alta Seguimiento", ex.Message);
            }

            catch (Exception ex) { 
            AuditarError(idLogueado,"Alta Seguimiento", ex.Message);
            }
        }

        
        private void AuditarError(int? idLogueado, string accion, string mensaje)
        {
            var aud = new Auditoria(idLogueado, accion, null, "ERROR: " + mensaje);
            _repoAuditoria.Auditar(aud);
        }

        private void AuditarExito(int usuarioId, int idSeguimiento, Seguimiento seguimiento)
        {
            var aud = new Auditoria(usuarioId, "ALTA_Seguimineto", idSeguimiento.ToString(), "Alta de seguimiento correcta: " + JsonSerializer.Serialize(seguimiento));
            _repoAuditoria.Auditar(aud);
        }

    }
}