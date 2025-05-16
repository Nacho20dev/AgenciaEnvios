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
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario : ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;
        private IRepositorioAuditoria _repoAuditoria;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario, IRepositorioAuditoria repoAuditoria)
        {
            _repoUsuario = repoUsuario;
            _repoAuditoria = repoAuditoria;
        }


        //Recibe un dtoAltaUsuario por parametro. Chequea que el mail ingresado no esté ya en la base,
        //chequea que el rol sea admin, mapea el DTO, chequea que la contraseña cumpla con el formato,
        //para luego agregarlo y auditarlo. En caso de error llama a los catch y audita el errro
        // +


        public void AltaUsuario(DTOAltaUsuario dto)
        {
            try
            {
               
                Usuario buscado = _repoUsuario.FindByEmail(dto.Email);
                if (buscado != null)
                {
                    throw new EmailYaExisteEx("Ya existe un usuario con ese email registrado");
                }

            
                if (_repoUsuario.EsAdmin(dto.LogueadoId) == true)
                {
                  
                    Usuario nuevo = MapperUsuario.DTOAltaToUsuario(dto);
                    nuevo.ValidarContrasenia(dto.Contrasenia);
                    int idEntidad = _repoUsuario.Add(nuevo);
                    Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", idEntidad.ToString(), "Alta correcta: " + JsonSerializer.Serialize(nuevo));
                    _repoAuditoria.Auditar(aud);
                }
                else
                {
                    
                    Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: El usuario no es Administrador");
                    _repoAuditoria.Auditar(aud);
                    throw new Exception("El usuario no es Administrador");
                }
            }
            catch (NombreVacioEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (ApellidoVacioEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (ContraseniaVaciaEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (ContraseniaCortaEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (NoCumpleCaracteresEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (EmailInvalidoEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (UsuarioNoValidoEx e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (EmailYaExisteEx e) 
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
            catch (Exception e)
            {
                Auditoria aud = new Auditoria(dto.LogueadoId, "ALTA", null, "ERROR: " + e.Message);
                _repoAuditoria.Auditar(aud);
                throw e;
            }
        }




    }
}

