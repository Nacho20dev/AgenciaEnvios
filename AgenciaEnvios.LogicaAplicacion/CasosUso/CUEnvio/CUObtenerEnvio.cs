using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaAccesoDatos.Repositorios;
using AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{
   public class CUObtenerEnvio : ICUObtenerEnvio
    {
        private IRepositorioEnvio _repositorioEnvio;

        public CUObtenerEnvio(IRepositorioEnvio repositorioEnvio)
        {
            _repositorioEnvio = repositorioEnvio;
        }


        //recibe un id para buscar en la base el objeto y luego lo mapea para poder devolver un DTO. 
        public DTOAltaEnvio ObtenerEnvio(int id)
        {
            Envio e = _repositorioEnvio.FindById(id);

            return MapperEnvio.EnvioToDTOEnvio(e);
        }

      
    }
}
