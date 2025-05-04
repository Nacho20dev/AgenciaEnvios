using AgenciaEnvios.DTOs.DTOs.DTOAgencia;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.Mappers
{
    public class MapperAgencia
    {







        public static List<DTOAgencia> FromListAgenciaToListDtoAgencia(List<Agencia> agencias)
        {

            List<DTOAgencia> ret = new List<DTOAgencia>();

            foreach (Agencia agencia in agencias)
            {

                DTOAgencia dto = new DTOAgencia();
                dto.Id = agencia.Id;
                dto.Nombre = agencia.Nombre;
                ret.Add(dto);
            }
           
            return ret;

        }





    }
}
