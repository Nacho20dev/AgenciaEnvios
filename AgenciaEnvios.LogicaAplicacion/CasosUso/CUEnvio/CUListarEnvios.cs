using AgenciaEnvios.DTOs.DTOs.DTOAgencia;

using AgenciaEnvios.DTOs.DTOs.DTOEnvio;

using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{
    public class CUListarEnvios : ICUListarEnvios
    {
        private readonly IRepositorioEnvio _repositorioEnvio;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioAgencia _repositorioAgencia;

        public CUListarEnvios(IRepositorioEnvio repositorioEnvio, IRepositorioUsuario repositorioUsuario,
                              IRepositorioAgencia repositorioAgencia)
        {
            _repositorioEnvio = repositorioEnvio;
            _repositorioUsuario = repositorioUsuario;
            _repositorioAgencia = repositorioAgencia;
        }


        
        public List<DTOAltaEnvio> ListarEnvios()
        {
            try
            {
              
            List<Envio> envios = _repositorioEnvio.FindAll();
            List<DTOAltaEnvio> ret = MapperEnvio.FromListEnvioToListDTOEnvio(envios);
            return ret;
        
            }
            catch (EnvioAMostrarVacioEx e) 
            {
                throw e;
            }




        }
    }
}


