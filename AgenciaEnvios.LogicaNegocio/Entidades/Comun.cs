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

        //Metodo que sobrescribe el metodo de la clase padre (envio) para finalizar envio según cada caso.
        //En esta caso, para el envío comun, carga la fecha actual para poder registrarla en el seguimento
        //y cambia el estado del envio a finalizado. Luego genera un seguimiento en el que cargará por defecto
        //el comentario, cargará el usuario que recibió por parametro y cargará la fecha anteriormente obtenida. 
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
