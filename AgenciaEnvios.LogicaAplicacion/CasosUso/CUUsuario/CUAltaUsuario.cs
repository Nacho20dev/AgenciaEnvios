using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUUsuario
{
    public class CUAltaUsuario:ICUAltaUsuario
    {
        private IRepositorioUsuario _repoUsuario;

        public CUAltaUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }


        public void AltaEmpleado(DTOAltaUsuario dto)
        {

            Usuario buscado = _repoUsuario.FindByEmail(dto.Email);
            if (buscado != null)
            {
                throw new EmailYaExisteException("EL email ya existe");
            }

            try
            {
                Usuario nuevo = MapperUsuario.ToUsuario(dto);
                _repoUsuario.Add(nuevo);
            }
            catch (Exception e)
            {
                //Audito el problema
                throw e;
            }

        }
    }
}
}
