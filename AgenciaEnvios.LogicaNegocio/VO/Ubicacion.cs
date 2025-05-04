using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.VO
{
    [Serializable]
    [ComplexType]
    public record Ubicacion
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }


        public Ubicacion()
        {

        }

        public Ubicacion(double latitud, double longitud)
        {
            Latitud = latitud;
            Longitud = longitud;
        }

    }
}
