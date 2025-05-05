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
    public class CUObtenerObjetoEnvio : ICUObtenerObjetoEnvio
    {

        private IRepositorioEnvio _repositorioEnvio;

        public CUObtenerObjetoEnvio(IRepositorioEnvio repositorioEnvio) 
        {
            _repositorioEnvio = repositorioEnvio;
        }
        public Envio ObtenerObjetoEnvio(int id)
        {
            return _repositorioEnvio.FindById(id);
        }
    }
}
