using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.VO
{
    [Serializable]
    public record DireccionPostal
    {
        public string Destinatario { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public int CodigoPostal { get; set; }
        public string Departamento { get; set; }

    

    
    public DireccionPostal()
        {

        }

        public DireccionPostal(string destinatario, string calle, string numero, string ciudad, int codigoPostal, string? departamento)
        {
            Destinatario = destinatario;
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            CodigoPostal = codigoPostal;
            Departamento = departamento;
        }

    }

}
