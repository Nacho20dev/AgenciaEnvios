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

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CULogin : ICULogin
    {
        
        
            private IRepositorioUsuario _repoUsuario;

            public CULogin(IRepositorioUsuario repoUsuario)
            {
                _repoUsuario = repoUsuario;
            }

        public DTOUsuario VerificarDatosParaLogin(DTOUsuario dto)
        {
            try
            {
                Usuario u = _repoUsuario.FindByEmail(dto.Email);

                bool coincideElPassword = Utilidades.Crypto.VerificaPasswordConBcrypt(dto.Contrasenia, u.Contrasenia);

                if (coincideElPassword && u!=null)
                {
                    DTOUsuario ret = new DTOUsuario();
                    ret.Id = u.Id;
                    ret.Rol = u.Rol.ToString();
                    return ret;
                }
                else
                {

                    throw new DatosNoValidosEx();
                    
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        
    }
    }
