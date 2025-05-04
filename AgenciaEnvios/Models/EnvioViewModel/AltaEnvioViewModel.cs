using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgenciaEnvios.WebApp.Models.EnvioViewModel
{
    public class AltaEnvioViewModel
    {
        public DTOAltaEnvio Dto { get; set; }

        public List<SelectListItem> TiposDeEnvios = new List<SelectListItem>() {
            new SelectListItem{ Text ="Comun", Value = "Comun" },
            new SelectListItem{ Text ="Urgente", Value = "Urgente" },
        };

        public List<SelectListItem> AgenciasOrigen = new List<SelectListItem>();
        public List<SelectListItem> AgenciasDestino = new List<SelectListItem>();

    }
}
