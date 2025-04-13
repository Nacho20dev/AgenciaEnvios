using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaNegocio.Entidades
{
    public class Usuario
    {
        
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }

        public string Email { get; set; }
        public string Rol { get; set; }

        public Usuario(string nombre, string apellido, string contrasenia, string email, string rol)
        {
            Nombre = nombre;
            Apellido = apellido;
            Contrasenia = contrasenia;
            Email = email;
            Rol = rol;

            Validar();



        }

        public Usuario()
        {
            
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new NombreVacioEx();
            }

            if (string.IsNullOrEmpty(Apellido))
            {
                throw new ApellidoVacioEx();
            }

            if (string.IsNullOrEmpty(Contrasenia))
            {
                throw new ContraseniaVaciaEx("La contraseña no puede estar vacia");
            }

            if(Contrasenia.Length <8)
            {
                throw new ContraseniaCortaEx("La contraseña debe tener 8 caracteres como mínimo");
            }

            
            bool CumpleCaracteres(string input)
            {
                bool tieneMayuscula = false;
                bool tieneMinuscula = false;
                bool tieneNumero = false;
                bool tieneEspecial = false;

                foreach (char c in input)
                {
                    if (char.IsUpper(c))
                        tieneMayuscula = true;
                    else if (char.IsLower(c))
                        tieneMinuscula = true;
                    else if (char.IsDigit(c))
                        tieneNumero = true;
                    else
                        tieneEspecial = true;
                }

                return tieneMayuscula && tieneMinuscula && tieneNumero && tieneEspecial;
            }

            if (!CumpleCaracteres(Contrasenia))
            {
                throw new NoCumpleCaracteresEx("La contraseña debe contener al menos una minúscula, una mayúscula,un número y un simbolo");
            }



            if(!Email.Contains("@") && !Email.EndsWith(".com"))
            {
                throw new EmailInvalidoEx("El email debe contener arroba y terminar en .com");
            }


            if(Rol != "Administrador" && Rol != "Funcionario" && Rol != "Cliente")
            {
                throw new UsuarioNoValidoEx("Debe seleccionar un usuario");
            }
        }


    }
}
