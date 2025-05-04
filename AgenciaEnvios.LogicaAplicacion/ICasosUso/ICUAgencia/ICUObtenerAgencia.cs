using AgenciaEnvios.DTOs.DTOs.DTOAgencia;
using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.DTOs.Mappers;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAplicacion.ICasosUso.ICUAgencia
{
    public interface ICUObtenerAgencia
    {
        List<DTOAgencia> ObtenerAgencias();
    }


}

