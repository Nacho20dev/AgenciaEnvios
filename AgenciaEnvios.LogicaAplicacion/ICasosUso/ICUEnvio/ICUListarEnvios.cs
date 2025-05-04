
using AgenciaEnvios.DTOs.DTOs.DTOEnvio;

using AgenciaEnvios.LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUEnvio
{
    


    public interface ICUListarEnvios
    {
        List<DTOAltaEnvio> ListarEnvios();
    }

}
