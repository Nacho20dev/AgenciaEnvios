using AgenciaEnvios.DTOs.DTOs.DTOSeguimiento;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.Mappers
{
    public class MapperSeguimiento
    {
        public static List<DTOSeguimiento> FromListSeguimientosToDTOListSeguimientos(List<Seguimiento> seguimientos) 
        { 
            List<DTOSeguimiento> dtoSeguimientos = new List<DTOSeguimiento>();
            foreach(Seguimiento seguimiento in seguimientos) 
            {
                DTOSeguimiento dto = new DTOSeguimiento();
                dto = FromSeguimientoToDTOSeguimiento(seguimiento);
                dtoSeguimientos.Add(dto);

             }    
            
            
            
            return dtoSeguimientos;

        }

        public static DTOSeguimiento FromSeguimientoToDTOSeguimiento(Seguimiento seg)
        {
            DTOSeguimiento dto = new DTOSeguimiento();
            dto.comentario = seg.Comentario;
            dto.IdEnvio = seg.EnvioId;
            dto.IdLogueado = seg.Usuario.Id;
            
            return dto; 
        }


        public static Seguimiento FromDTOSeguimientoToSeguimiento(DTOSeguimiento dto)
        {
            Seguimiento se = new Seguimiento();
            se.Comentario = dto.comentario;
            se.EnvioId = dto.IdEnvio;
            return se;
        }
    }
}
