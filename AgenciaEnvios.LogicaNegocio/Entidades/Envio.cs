﻿using AgenciaEnvios.LogicaNegocio.CustomExceptions.EnvioExceptions;
using AgenciaEnvios.LogicaNegocio.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    [Serializable]
    public abstract class Envio
    {
        public int Id { get; set; }
        public string NroTracking { get; set; }
        public Usuario? Usuario { get; set; }
        public string EmailCliente { get; set; }
        public double Peso { get; set; }
        public EstadoEnvios Estado { get; set; }
        public List<Seguimiento> Seguimientos { get; set; }
        public Agencia AgenciaOrigen { get; set; }
        public int AgenciaOrigenId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public Envio()
        {
            
            FechaInicio = DateTime.Now;
            Seguimientos = new List<Seguimiento>();
            Seguimiento seguimiento = new Seguimiento { Comentario = "En Agencia de Origen", Usuario = Usuario, Fecha = FechaInicio };
            Estado = EstadoEnvios.En_Proceso;
        }

        public Envio(int id, string nroTracking, Usuario usuario, string emailCliente, double peso,
                 List<Seguimiento> seguimientos, Agencia agenciaOrigen, int agenciaOrigenId)
        {
            Id = id;
            NroTracking = nroTracking;
            Usuario = usuario;
            EmailCliente = emailCliente;
            Peso = peso;
            Estado = EstadoEnvios.En_Proceso;
            Seguimientos = seguimientos ?? new List<Seguimiento>();
            AgenciaOrigen = agenciaOrigen;
            AgenciaOrigenId = agenciaOrigenId;
            FechaInicio = DateTime.Now;
            
            
        }

        public void EnvioNoEncontradoEx(Envio envio)
        {
            if (envio == null)
            {
                throw new EnvioNoEncontradoEx("El envío no fue encontrado.");
            }

        }

        //Metodo en el que se recibe un nroTracking y se chequea que cumpla con el formato.
        //El formato está establecido en el string patron que luego es utilizado cuando se llama al método
        //IsMAtch de Regex. Este devolverá true si cumple con el formato o false en caso contrario. 
        public static bool EsGuidValido(string NroTracking)
        {
          

            // Patrón exacto para validar el formato de un GUID
            string patron = @"^[a-fA-F0-9]{8}-" +
                            @"[a-fA-F0-9]{4}-" +
                            @"[a-fA-F0-9]{4}-" +
                            @"[a-fA-F0-9]{4}-" +
                            @"[a-fA-F0-9]{12}$";

            return Regex.IsMatch(NroTracking, patron);

           

        }



        public void PesoInvalidoEx(double peso)
        {
            if (Peso <= 0)
            {
                throw new PesoInvalidoEx("El peso del envío no puede ser cero o negativo.");
            }

        }

        public void EstadoInvalidoEx(EstadoEnvios estado)
        {
            if (!Enum.IsDefined(typeof(EstadoEnvios), Estado))
            {
                throw new EstadoInvalidoEx("El estado del envío no es válido.");
            }

        }

        public void ValidarFechasEnvio(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                throw new ErrorDeFechaEx("La fecha de inicio no puede ser posterior a la fecha de fin.");
            }
        }

        //Metodo abstract que será sobreescrito en cada clase hija según sea el tipo de envio.
        public abstract void FinalizarEnvio(Usuario usuario);
        
    }
}
