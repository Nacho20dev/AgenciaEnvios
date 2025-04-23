using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.InterfacesRepositorios
{
   public interface IRepositorioAuditoria
    {
        void Auditar(Auditoria nueva);
    }
}
