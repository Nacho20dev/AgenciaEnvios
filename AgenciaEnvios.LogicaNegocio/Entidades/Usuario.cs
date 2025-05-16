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

        public int Id { get; set; }
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

            


            if (!Email.Contains("@") && !Email.EndsWith(".com"))
            {
                throw new EmailInvalidoEx("El email debe contener arroba y terminar en .com");
            }


            if (Rol != "Administrador" && Rol != "Funcionario" && Rol != "Cliente")
            {
                throw new UsuarioNoValidoEx("Debe seleccionar un usuario");
            }


        }

        //Recibe por parametro la contraseña a validar. Valida que no está vacia, que tenga una longitud de más
        //de 8 caracteres y llama a la función cumpleCaracteres en la cual se chequea que tenga mayuscula, minuscula,
        //numero y simbolo. Si no lanza ninguna de las excepciones no hace nada, simplemente permite que siga adelante.
        //
        public  void ValidarContrasenia(string contrasenia)
        {
            if (string.IsNullOrEmpty(contrasenia))
            {
                throw new ContraseniaVaciaEx("La contraseña no puede estar vacía.");
            }
            if (contrasenia.Length < 8)
            {
                throw new ContraseniaCortaEx("La contraseña debe tener 8 caracteres como mínimo");
            }
         
            if (!CumpleCaracteres(contrasenia))
            {
                throw new NoCumpleCaracteresEx("La contraseña debe contener al menos una minúscula, una mayúscula,un número y un simbolo");
            }
        }


            //recibe la contraseña para chequear que tenga mayuscula, minusculas, numero y caracter especial.
            //Incializa un bool para cada cosa a chequear en false y a medida que va recorriendo la contraseña
            //y que va cumpliendo con alguno de las cosas a chequear pasa a true el bool correspondiente.
            //Al final devolverá true si todos los bool estaban en true, o false si alguno estaba en false
            public bool CumpleCaracteres(string input)
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

    }
}
