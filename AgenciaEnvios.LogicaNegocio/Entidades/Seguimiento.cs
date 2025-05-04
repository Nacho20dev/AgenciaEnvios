using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    public class Seguimiento
    {
        public int? Id { get; set; }
        public string Comentario { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
    

    public Seguimiento()
        {

        }

        public Seguimiento(int? id, string comentario, Usuario usuario, DateTime fecha)
        {
            Id = id;
            Comentario = comentario;
            Usuario = usuario;
            Fecha = fecha;
        }
    }
}
