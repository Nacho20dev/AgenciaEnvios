using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.Enumerados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.DTOs.DTOEnvio
{
    public class DTOAltaEnvio
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? NroTracking { get; set; }

       public int? IdLogueado { get; set; }

       public string? NombreEmpleado { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email no cumple formato")]
        public string EmailCliente { get; set; }

        [Required(ErrorMessage = "El peso es obligatorio.")]
       
        public double Peso { get; set; }

        public EstadoEnvios Estado { get; set; }

        public string? EnProceso { get; set; }
        public string TipoEnvio { get; set; }

        public List<Seguimiento> Seguimientos { get; set; }
        
        public List<int> IdsAgencias { get; set; }

        public Agencia? AgenciaOrigen {  get; set; }
        public int IdAgenciaOrigen { get; set; }
        public Agencia? AgenciaDestino { get; set; }
        public int? IdAgenciaDestino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "El Destinatario es obligatorio.")]
        public string? Destinatario { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Calle {  get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? NroPuerta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Ciudad {  get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int? CodigoPostal { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Departamento { get; set; }

        public bool Eficiente { get; set; }

    }

}
