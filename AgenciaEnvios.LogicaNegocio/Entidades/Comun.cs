using AgenciaEnvios.LogicaNegocio.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    [Serializable]
    public class Comun : Envio
    {
        public Agencia AgenciaDestino { get; set; }
        public int? AgenciaDestinoId { get; set; }

        public Comun() : base() { } 
        public Comun(int id, string nroTracking, Usuario usuario, string emailCliente, double peso,
                     List<Seguimiento> seguimientos, Agencia agenciaOrigen, int agenciaOrigenId,  Agencia agenciaDestino , int agenciaDestinoId)
            : base(id, nroTracking, usuario, emailCliente, peso,  seguimientos, agenciaOrigen,agenciaOrigenId)
        {
            AgenciaDestinoId = agenciaDestinoId;
            AgenciaDestino = agenciaDestino;
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
        }
    }

}
