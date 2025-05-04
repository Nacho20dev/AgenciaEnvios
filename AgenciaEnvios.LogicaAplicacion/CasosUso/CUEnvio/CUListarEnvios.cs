using AgenciaEnvios.DTOs.DTOs.DTOAgencia;

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
    public class CUListarEnvios : ICUListarEnvios
    {
        private readonly IRepositorioEnvio _repositorioEnvio;

        public CUListarEnvios(IRepositorioEnvio repositorioEnvio)
        {
            _repositorioEnvio = repositorioEnvio;
        }



        public List<DTOAltaEnvio> ListarEnvios()
        {

            List<Envio> envios = _repositorioEnvio.FindAll();
            return MapperEnvio.FromListEnvioToListDtoEnvio(envios);





        }
    }
}


