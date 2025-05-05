using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio
{
    public interface ICUObtenerObjetoEnvio
    {
        Envio ObtenerObjetoEnvio(int Id); 
    }
}
