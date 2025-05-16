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

        //Recibe del controler el id del envio y el usuario. Recupera del repo el envio a partir del id del 
        //recibido y llama al metodo de la clase FinalizarEnvio que recibe por parametro un usuario para
        //poder registrarlo en el seguimiento que se genera al finalizar el envio. Luego realiza el update para que
        //quede actualizado en la base. 
        public void FinalizarEnvio(int envioId, Usuario usuario)
        {
            int? eID = (int?)envioId;
            Envio envio = _repositorioEnvio.FindById(eID);

            envio.FinalizarEnvio(usuario);


            _repositorioEnvio.Update(envio);
        }

    }
}
