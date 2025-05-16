using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using AgenciaEnvios.LogicaNegocio.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{




    public class CUAltaEnvio : ICUAltaEnvio
    {
        
        private IRepositorioEnvio _repositorioEnvio;
            private IRepositorioUsuario _repositorioUsuario;
            private IRepositorioAgencia _repositorioAgencia;
            private IRepositorioAuditoria _repoAuditoria;

            public CUAltaEnvio(IRepositorioEnvio repoEnvio, IRepositorioUsuario repoUsuario, IRepositorioAgencia repoAgencia, IRepositorioAuditoria repoAuditoria)
            {
                _repositorioEnvio = repoEnvio;
                _repositorioUsuario = repoUsuario;
                _repositorioAgencia = repoAgencia;
                _repoAuditoria = repoAuditoria;
            }


        //Metodo de alta que trae un DTO del controler, hace las validadiones correspondientes,
        //se trae del repo los objetos que necesita, mapea el DTO y llama a 
        //metodo asignarPropiedadesComunes que carga en el envio los objetos previamente cargados.
        //Luego agrega a la base ese envio creado
        public void AltaEnvio(DTOAltaEnvio dto)
        {
            try
            {
                _repositorioUsuario.ValidarFormatoEmail(dto.EmailCliente);

                var cliente = _repositorioUsuario.FindByEmail(dto.EmailCliente)
                                    ?? throw new EmailNoRegistradoEx("El cliente ingresado no está registrado.");

                var usuario = _repositorioUsuario.FindById(dto.IdLogueado)
                                 ?? throw new UsuarioNoValidoEx("Usuario no encontrado.");

                var agenciaOrigen = _repositorioAgencia.ObtenerAgencia(dto.IdAgenciaOrigen)
                                            ?? throw new AgenciaNoEncontradaEx("Agencia de origen no encontrada.");

                Agencia? agenciaDestino = null;
                if (dto.IdAgenciaDestino.HasValue)
                {
                    agenciaDestino = _repositorioAgencia.ObtenerAgencia(dto.IdAgenciaDestino.Value)
                                        ?? throw new AgenciaNoEncontradaEx("Agencia destino no encontrada.");
                }
                else if (dto.TipoEnvio.Equals("Comun", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("La Agencia de Destino es obligatoria para envíos Comunes.");
                }
                else if (!dto.TipoEnvio.Equals("Urgente", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("El tipo de envío no es válido.");
                }

                var envio = MapperEnvio.FromDtoAltaEnvioToEnvio(dto, usuario, agenciaOrigen, agenciaDestino);
                AsignarPropiedadesComunes(envio, usuario, agenciaOrigen);

                if (envio is Comun envioComun && agenciaDestino != null)
                {
                    envioComun.AgenciaDestinoId = agenciaDestino.Id;
                    envioComun.AgenciaDestino = agenciaDestino;
                }
               

                ValidarEnvio(envio);

                int idEnvio = _repositorioEnvio.Add(envio);
                AuditarExito(usuario.Id, idEnvio, envio);
            }
            catch (UsuarioNoValidoEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (AgenciaNoEncontradaEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (PesoInvalidoEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (EstadoInvalidoEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (EmailInvalidoEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (EmailNoRegistradoEx e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (InvalidOperationException e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", e.Message); throw; }
            catch (Exception e) { AuditarError(dto.IdLogueado, "ALTA_ENVIO", "ERROR inesperado: " + e.Message); throw; }
        }


        // Carga en el envio pasado por parametro el usuario, la agencia origen y su id, a partir de lo que también
        // recibe por parametro.
        private void AsignarPropiedadesComunes(Envio envio, Usuario usuario, Agencia agenciaOrigen)
        {
            envio.Usuario = usuario;
            envio.AgenciaOrigenId = agenciaOrigen.Id;
            envio.AgenciaOrigen = agenciaOrigen;
        }

        private void ValidarEnvio(Envio envio)
        {
            envio.PesoInvalidoEx(envio.Peso);
            envio.EstadoInvalidoEx(envio.Estado);

            if (envio.FechaFin.HasValue)
            {
                envio.ValidarFechasEnvio(envio.FechaInicio, envio.FechaFin.Value);
            }
        }

        private void AuditarError(int? idLogueado, string accion, string mensaje)
        {
            var aud = new Auditoria(idLogueado, accion, null, "ERROR: " + mensaje);
            _repoAuditoria.Auditar(aud);
        }

        private void AuditarExito(int usuarioId, int idEnvio, Envio envio)
        {
            var aud = new Auditoria(usuarioId, "ALTA_ENVIO", idEnvio.ToString(), "Alta de envío correcta: " + JsonSerializer.Serialize(envio));
            _repoAuditoria.Auditar(aud);
        }
    }

}




           


    



