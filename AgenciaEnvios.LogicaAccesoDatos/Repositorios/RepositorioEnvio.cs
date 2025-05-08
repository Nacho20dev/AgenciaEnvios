using AgenciaEnvios.LogicaAccesoDatos.Migrations;
using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
using AgenciaEnvios.LogicaNegocio.Entidades;
using AgenciaEnvios.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos.Repositorios
{
    public class RepositorioEnvio : IRepositorioEnvio
    {
        private  ApplicationDBContext _context;
        

        public RepositorioEnvio(ApplicationDBContext context)
        {
            _context = context;
        }

        public int Add(Envio nuevo)
        {
            _context.Envios.Add(nuevo);
            _context.SaveChanges();
            return nuevo.Id;
        }

        public List<Envio> FindAll()
        {
            return _context.Envios.ToList();
        }

        public Envio FindById(int? id)
        {
           
            var urgente = _context.Urgentes
                .Include(e => e.AgenciaOrigen)
                .Include(e => e.DireccionPostal) 
                .FirstOrDefault(e => e.Id == id);

            if (urgente != null)
                return urgente;

            var comun = _context.Comunes
                .Include(e => e.AgenciaOrigen)
                .Include(e => e.AgenciaDestino) 
                .FirstOrDefault(e => e.Id == id);

            return comun;
        }



        public Envio FindByNroTracking(string nroTracking)
        {
            return _context.Envios.FirstOrDefault(e => e.NroTracking == nroTracking);
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Envio obj)
        {

            var envio = _context.Envios.SingleOrDefault(e => e.Id == obj.Id);

            if (envio == null)
                throw new InvalidOperationException("Usuario no encontrado.");



            _context.SaveChanges();
            return envio.Id;
        }



        }
    }


