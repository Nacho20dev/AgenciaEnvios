using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private ApplicationDBContext _context;
        public RepositorioUsuario(ApplicationDBContext context)
        {
            _context = context;
        }

        public void Add(Usuario nuevo)
        {
            _context.Usuarios.Add(nuevo);
            _context.SaveChanges();
        }

        public List<Usuario> FindAll()
        {
         return _context.Usuarios.ToList();
        }



        public Usuario FindByEmail(string email)
        {
            if (email == null) throw new ArgumentException("Datos incorrectos");
            return _context.Usuarios.SingleOrDefault(x => x._email == email);
        }


        public Usuario FindById(int id)
        {
            return _context.Usuarios.Find(id);
        }

        public void Remove(int id)
        {
          _context.Remove(id);
            _context.SaveChanges();
        }



        //Recibe un usuario previamente mapeado. Lo busca por mail,
        //verifica que no deje ningún dato de los editables vacio
        //y lo agrega al context. 
        //Ver si vamos a agregar rol para caso administrador. Sino estaría pronto!
        public void Update(Usuario usu)
        {
            var usuario = _context.Usuarios.SingleOrDefault(u => u._email == usu._email);

            if (usuario == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            if (string.IsNullOrEmpty(usuario._nombre))
                throw new NombreVacioEx();

            if (string.IsNullOrEmpty(usuario._apellido))
                throw new ApellidoVacioEx();

            usuario._nombre = usu._nombre;
            usuario._apellido = usu._apellido;


            _context.SaveChanges();
        }
    }
}
