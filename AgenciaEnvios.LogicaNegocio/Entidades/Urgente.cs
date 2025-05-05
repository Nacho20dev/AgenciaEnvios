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
