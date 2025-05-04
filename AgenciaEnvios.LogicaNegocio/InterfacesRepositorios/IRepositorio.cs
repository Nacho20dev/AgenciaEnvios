using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorio<T> where T : class
    {
        int Add(T nuevo);
        T FindById(int? id);
        int Remove(int id);
        List<T> FindAll();

        int Update(T obj);
    }
}
