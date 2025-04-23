using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios
{
    public class RepositorioAuditoria : IRepositorioAuditoria
    {
        private ApplicationDBContext _context;

        public RepositorioAuditoria(ApplicationDBContext context)
        {
            _context = context;
        }


        public void Auditar(Auditoria nueva)
        {
            _context.Auditorias.Add(nueva);
            _context.SaveChanges();
        }
    }
}
