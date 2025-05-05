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


    public class CUFinalizarEnvio: ICUFinalizarEnvio
    {

        private IRepositorioEnvio _repositorioEnvio;

        public CUFinalizarEnvio(IRepositorioEnvio repositorioEnvio)
        {
            _repositorioEnvio = repositorioEnvio;
        }

        public void FinalizarEnvio(int envioId, Usuario usuario)
        {
            int? eID = (int?)envioId;
            Envio envio = _repositorioEnvio.FindById(eID);

            envio.FinalizarEnvio(usuario);


            _repositorioEnvio.Update(envio);
        }

    }
}
