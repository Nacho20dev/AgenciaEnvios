using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.Mappers
{
    public class MapperUsuario
    {
        public static Usuario DTOToUsuario(DTOAltaUsuario dto)
        {
            Usuario u = new Usuario(dto.nombre, dto.apellido, dto.contrasenia, dto.email, dto.rol);


            return u;
        }

        public static DTOUsuario UsuarioToDTOUsuario(Usuario usu)
        {

            DTOUsuario dto = new DTOUsuario();

            dto.id = usu._id;
            dto.nombre = usu._nombre;
            dto.apellido = usu._apellido;
            dto.contrasenia = usu._contrasenia;
            dto.email = usu._email;
            dto.rol = usu._rol;


            return dto;
        }
    }
}
