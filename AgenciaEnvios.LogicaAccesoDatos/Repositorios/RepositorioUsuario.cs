using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

    public int Add(Usuario nuevo)
    {
        _context.Usuarios.Add(nuevo);
        _context.SaveChanges();
        return nuevo.Id;
    }

    public List<Usuario> FindAll()
    {
        return _context.Usuarios.ToList();
    }



        public Usuario FindByEmail(string email)
    {
        if (email == null) throw new ArgumentException("Datos incorrectos");
        return _context.Usuarios.SingleOrDefault(x => x.Email == email);
    }


    public Usuario FindById(int? id)
    {
        return _context.Usuarios.Find(id);
    }

        public string GetNombreCompleto(int id)
        {
            Usuario u = FindById(id);
            string NombreCompleto = u.Nombre + " " + u.Apellido;
            return NombreCompleto;
            
        }

        public int Remove(int id)
        {
            var usuario = FindById(id);
            if (usuario == null)
            {
                throw new InvalidOperationException($"No se encontró un usuario con ID {id}.");
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return id;
        }




      

        public int Update(Usuario usu)
        {


            var usuario = _context.Usuarios.SingleOrDefault(u => u.Id == usu.Id);

            if (usuario == null)
              throw new InvalidOperationException("Usuario no encontrado.");

            if (string.IsNullOrEmpty(usuario.Nombre))
                throw new NombreVacioEx();

            if (string.IsNullOrEmpty(usuario.Apellido))
                throw new ApellidoVacioEx();

            usuario.Nombre = usu.Nombre;
            usuario.Apellido = usu.Apellido;
            usuario.Email = usu.Email;

            _context.SaveChanges();
            return usuario.Id;
        }

        public bool EsAdmin(int id)
        {
            bool retorno = false;
            Usuario usu = FindById(id);

            if (usu != null)
            {
                if (usu.Rol == "Administrador")
                {
                    retorno = true;
                }
            }
         

            return retorno;
        }

        public void ValidarFormatoEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                !email.Contains("@") ||
                !email.EndsWith(".com"))
            {
                throw new EmailInvalidoEx("El email debe contener '@' y terminar en '.com'");
            }
        }
    }
}

