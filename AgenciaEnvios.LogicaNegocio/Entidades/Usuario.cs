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
        public int _id {  get; set; }
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string _contrasenia { get; set; }

        public string _email { get; set; }
        public string _rol { get; set; }

        public Usuario(string Nombre, string Apellido, string Contrasenia, string email, string Rol)
        {
            _nombre = Nombre;
            _apellido = Apellido;
            _contrasenia = Contrasenia;
            _email = email;
            _rol = Rol;

            Validar();



        }

        public Usuario()
        {
            
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new NombreVacioEx();
            }

            if (string.IsNullOrEmpty(_apellido))
            {
                throw new ApellidoVacioEx();
            }

            if (string.IsNullOrEmpty(_contrasenia))
            {
                throw new ContraseniaVaciaEx("La contraseña no puede estar vacia");
            }

            if(_contrasenia.Length <8)
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

            if (!CumpleCaracteres(_contrasenia))
            {
                throw new NoCumpleCaracteresEx("La contraseña debe contener al menos una minúscula, una mayúscula,un número y un simbolo");
            }



            if(!_email.Contains("@") && !_email.EndsWith(".com"))
            {
                throw new EmailInvalidoEx("El email debe contener arroba y terminar en .com");
            }


            if(_rol != "Administrador" && _rol != "Funcionario" && _rol != "Cliente")
            {
                throw new UsuarioNoValidoEx("Debe seleccionar un usuario");
            }
        }


    }
}
