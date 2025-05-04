using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioAgencia
    {
        List<Agencia> FindAll();
        Agencia FindById(int id);
        Agencia ObtenerAgencia(int id);
    }
}
