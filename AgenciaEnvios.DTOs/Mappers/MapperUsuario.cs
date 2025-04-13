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
            Usuario u = new Usuario(dto.Nombre, dto.Apellido, dto.Contrasenia, dto.Email, dto.Rol);


            return u;
        }

        public static DTOUsuario UsuarioToDTOUsuario(Usuario usu)
        {

            DTOUsuario dto = new DTOUsuario();

            dto.Id = usu.Id;
            dto.Nombre = usu.Nombre;
            dto.Apellido = usu.Apellido;
            dto.Contrasenia = usu.Contrasenia;
            dto.Email = usu.Email;
            dto.Rol = usu.Rol;


            return dto;
        }
    }
}
