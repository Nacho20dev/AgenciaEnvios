using AgenciaEnvios.LogicaNegocio.Enumerados;
using AgenciaEnvios.LogicaNegocio.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    [Serializable]
    public class Urgente : Envio
    {
        public DireccionPostal DireccionPostal { get; set; }
        public bool Eficiente { get; set; }

        public Urgente() : base() { } 
        public Urgente(int id, string nroTracking, Usuario usuario, string emailCliente, double peso, 
                       List<Seguimiento> seguimientos, Agencia agenciaOrigen, int agenciaOrigenId,DireccionPostal direccionPostal, bool eficiente
                      )
            : base(id, nroTracking, usuario, emailCliente, peso, seguimientos, agenciaOrigen,agenciaOrigenId)
        {
            DireccionPostal = direccionPostal;
            Eficiente = eficiente;

        }

        //Metodo que sobrescribe el metodo de la clase padre (envio) para finalizar envio según cada caso.
        //En esta caso, para el envío urgente, carga la fecha actual para poder registrarla en el seguimento
        //y cambia el estado del envio a finalizado. Luego genera un seguimiento en el que cargará por defecto
        //el comentario, cargará el usuario que recibió por parametro y cargará la fecha anteriormente obtenida.
        //Calcula la diferencia entre fecha fin y de inicio para para calcular si es eficiente o no. 
        public override void FinalizarEnvio(Usuario usuario)
        {
            FechaFin = DateTime.Now;
            Estado = EstadoEnvios.Finalizado;

            Seguimientos.Add(new Seguimiento
            {
                Comentario = "Envio entregado",
                Usuario = usuario,
                Fecha = FechaFin.Value
            });

            TimeSpan diferencia = FechaFin.Value - FechaInicio;
            Eficiente = diferencia.TotalHours <= 24;
        }

    }

}
