using AgenciaEnvios.DTOs.DTOs.DTOAgencia;
using AgenciaEnvios.DTOs.DTOs.DTOEnvio;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.VO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace AgenciaEnvios.LogicaAplicacion.CasosUso.CUEnvio
{
    public class MapperEnvio
    {
        public static Envio FromDtoAltaEnvioToEnvio(DTOAltaEnvio dto, Usuario usuario, Agencia agenciaOrigen, Agencia agenciaDestino)
        {
            Envio envioNuevo = null; // Inicializa en null para evitar posibles errores de compilación

            var nroTracking = dto.NroTracking ?? Guid.NewGuid().ToString();

            if (dto.TipoEnvio.Equals("Comun", StringComparison.OrdinalIgnoreCase)) // Considera la insensibilidad a mayúsculas
            {
                envioNuevo = new Comun(
                    dto.Id,
                    nroTracking,
                    usuario,
                    dto.EmailCliente,
                    dto.Peso,
                    new List<Seguimiento>(),
                    agenciaOrigen,
                    agenciaOrigen.Id,
                    agenciaDestino,
                    agenciaDestino.Id
                );
            }
            else if (dto.TipoEnvio.Equals("Urgente", StringComparison.OrdinalIgnoreCase))
            {
                DireccionPostal direccionPostal = new DireccionPostal(
                    dto.Destinatario,
                    dto.Calle,
                    dto.NroPuerta,
                    dto.Ciudad,
                    (int)dto.CodigoPostal,
                    dto.Departamento
                );

                envioNuevo = new Urgente(
                    dto.Id,
                    nroTracking,
                    usuario,
                    dto.EmailCliente,
                    dto.Peso,
                    new List<Seguimiento>(),
                    agenciaOrigen,
                    agenciaOrigen.Id,
                    direccionPostal,
                    dto.Eficiente
                );
            }
            if (envioNuevo is Urgente urgenteEnvio && urgenteEnvio.DireccionPostal == null)
            {
                throw new NullReferenceException("La dirección postal no fue asignada correctamente.");
            }

            return envioNuevo;
        }
        public static List<DTOAltaEnvio> FromListEnvioToListDTOEnvio(List<Envio> envios)
        {
            List<DTOAltaEnvio> ret = new List<DTOAltaEnvio>();

            foreach (Envio envio in envios)
            {
                ret.Add(EnvioToDTOEnvio(envio));
            }

            return ret;
        }


    


        public static DTOAltaEnvio EnvioToDTOEnvio(Envio envio)
        {
            var dto = new DTOAltaEnvio
            {
                Id = envio.Id,
                NroTracking = envio.NroTracking,
                EmailCliente = envio.EmailCliente,
                Peso = envio.Peso,
                Estado = envio.Estado,
                Seguimientos = envio.Seguimientos,
                FechaInicio = envio.FechaInicio,
                FechaFin = envio.FechaFin,
                AgenciaOrigen = envio.AgenciaOrigen,
                TipoEnvio = envio is Comun ? "Comun" : envio is Urgente ? "Urgente" : "Desconocido"
            };

            if (envio is Comun comun)
            {
                dto.AgenciaDestino = comun.AgenciaDestino;
            }

            if (envio is Urgente urgente && urgente.DireccionPostal != null)
            {
                dto.Destinatario = urgente.DireccionPostal.Destinatario;
                dto.Calle = urgente.DireccionPostal.Calle;
                dto.NroPuerta = urgente.DireccionPostal.Numero;
                dto.Ciudad = urgente.DireccionPostal.Ciudad;
                dto.CodigoPostal = urgente.DireccionPostal.CodigoPostal;
                dto.Departamento = urgente.DireccionPostal.Departamento;
            }

            return dto;
        }
    }

}
