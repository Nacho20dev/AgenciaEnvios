using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.LoginExceptions;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{

    public class CULogin : ICULogin
    {
        private IRepositorioUsuario _repoUsuario;

        public CULogin(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }


        //Trae por parametros un DTOUsuario que viene con email y contraseña. Los valida y luego los busca en la base.
        //En caso de encontrarlos y que la contraseña coincida (la hashea para comparar la de la base que está hasheada)
        //carga en el DTO el id, el nombre y el rol para luego retornarlo.
        public DTOUsuario VerificarDatosParaLogin(DTOUsuario dto)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(dto.Contrasenia))
                {
                    throw new ContraseniaVaciaEx("La contraseña no puede estar vacía.");
                }

                Usuario u = _repoUsuario.FindByEmail(dto.Email);

    
                if (u == null)
                {
                    throw new EmailNoRegistradoEx("El email ingresado no se encuentra registrado.");
                }

                bool coincideElPassword = Utilidades.Crypto.VerificaPasswordConBcrypt(dto.Contrasenia, u.Contrasenia);

                if (coincideElPassword)
                {
                    DTOUsuario ret = new DTOUsuario();
                    ret.Id = u.Id;
                    ret.Rol = u.Rol.ToString();
                    ret.Nombre = u.Nombre;
                    return ret;
                }
                else
                {
                    throw new DatosNoValidosEx("Contraseña incorrecta.");
                }
            }
            catch (EmailNoRegistradoEx e)
            {
                throw e;
            }

            catch(ContraseniaVaciaEx e) 
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
