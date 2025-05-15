using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios
{

    public class RepositorioAgencia : IRepositorioAgencia
        {
            private  ApplicationDBContext _context;

            public RepositorioAgencia(ApplicationDBContext context)
            {
                _context = context;
            }

        public List<Agencia> FindAll()
        {
            return _context.Agencias.ToList();
        }

        public Agencia FindById(int id)
        {
            return _context.Agencias.Find(id);
        }

      
            public Agencia ObtenerAgencia(int id)
        {
            return _context.Agencias.Where(a => a.Id.Equals(id)).SingleOrDefault();

        }
    }
    }



