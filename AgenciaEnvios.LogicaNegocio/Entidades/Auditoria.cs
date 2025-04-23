using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
  public class Auditoria
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Accion {  get; set; }
        public string? Entidad { get; set; }
        public string? Observaciones { get; set; }

        public DateTime Fecha { get; set; }

        public Auditoria()
        {
            Fecha = DateTime.Now;
        }
    }
}
