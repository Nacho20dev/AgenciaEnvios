using AgenciaEnvios.LogicaNegocio.VO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    public class Agencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DireccionPostal DireccionPostal { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public Agencia()
        {

        }
    public Agencia(int id, string nombre, DireccionPostal direccionPostal, Ubicacion ubicacion)
        {
            Id = id;
            Nombre = nombre;
            DireccionPostal = direccionPostal;
            Ubicacion = ubicacion;
        }
    }

}
