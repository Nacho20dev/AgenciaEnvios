using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.DTOs.DTOAgencia
{
    public class DTOAgencia
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }
        public Agencia AgenciaOrigen {  get; set; }
        public Agencia? AgenciaDestino { get; set; }
       
        public string? Destinatario { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Calle { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? NroPuerta { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Ciudad { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int? CodigoPostal { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string? Departamento { get; set; }
    }

}
