using AgenciaEnvios.DTOs.DTOs.DTOUsuario;
using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.DTOs.Mappers
{
    public class MapperUsuario
    {

        //Generamos un mapper especifico para mapear un DTo a un usuario para AltaUsuario
        //porque en el alta necesitamos hashear la contraseña y no asignamos un Id.
        //En el DTO que no es de alta si usamos un ID y no hasheamos la contraseña porque 
        //ya la tenemos hasheada
        public static Usuario DTOAltaToUsuario(DTOAltaUsuario dto)
        {
          

            string passHashed = Utilidades.Crypto.HashPasswordConBcrypt(dto.Contrasenia, 12);


            Usuario u = new Usuario(dto.Nombre, dto.Apellido, passHashed, dto.Email, dto.Rol);


            return u;
        }


        public static Usuario DTOToUsuario(DTOUsuario dto)
        {
            Usuario u = new Usuario(
                dto.Nombre,
                dto.Apellido,
                dto.Contrasenia, // ya hasheada
                dto.Email,
                dto.Rol
            );

            u.Id = dto.Id;
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

        public static List<DTOUsuario> FromListUsuarioToListDtoUsuario(List<Usuario> usuarios)
        {
            List<DTOUsuario> ret = new List<DTOUsuario>();

            foreach (Usuario usu in usuarios)
            {
                DTOUsuario dto = new DTOUsuario();
                dto.Id = usu.Id;
                dto.Nombre = usu.Nombre;
                dto.Apellido = usu.Apellido;
                dto.Contrasenia = usu.Contrasenia;
                dto.Email = usu.Email;
                dto.Rol = usu.Rol;

                ret.Add(dto); 
            }

            return ret;
        }



    }
}
