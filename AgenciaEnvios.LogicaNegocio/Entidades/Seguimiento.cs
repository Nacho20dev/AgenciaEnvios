using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
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

        public int? EnvioId { get; set; }

        
    

    public Seguimiento()
        {
            Fecha = DateTime.Now;
        }

        public Seguimiento(string comentario, Usuario usuario)
        {
            
            Comentario = comentario;
            Usuario = usuario;
            Fecha = DateTime.Now;
        }

        public void ComentarioVacio(String comentario)
        {
            if (string.IsNullOrEmpty(comentario))
            {
                throw new EnvioNoEncontradoEx("Debe ingresar un comentario.");
            }

        }

    }
}
