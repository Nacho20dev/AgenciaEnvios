using AgenciaEnvios.DTOs.DTOs.DTOSeguimiento;
using AgenciaEnvios.DTOs.DTOs.DTOEnvio;

namespace AgenciaEnvios.WebApp.Models.EnvioViewModel
{
    public class AltaSeguimientoViewModel
    {
        public DTOAltaEnvio DTOAltaEnvio { get; set; }

        public DTOSeguimiento  DTOSeguimiento { get; set; }
        
    }
}
