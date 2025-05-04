using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio
{
   
        public interface ICUObtenerEnvio
        {
            DTOAltaEnvio ObtenerEnvio(int id);
        }
    
}
