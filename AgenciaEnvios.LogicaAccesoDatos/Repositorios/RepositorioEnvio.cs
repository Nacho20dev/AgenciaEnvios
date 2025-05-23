﻿using AgenciaEnvios.LogicaNegocio.CustomExceptions.UsuarioExceptions;
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
                .Include(e => e.Seguimientos)
                .FirstOrDefault(e => e.Id == id);

            if (urgente != null)
                return urgente;

            var comun = _context.Comunes
                .Include(e => e.AgenciaOrigen)
                .Include(e => e.AgenciaDestino)
                .Include(e => e.Seguimientos)
                .FirstOrDefault(e => e.Id == id);

            return comun;
        }



        public Envio FindByNroTracking(string NroTracking)
        {
            var urgente = _context.Urgentes
               .Include(e => e.AgenciaOrigen)
               .Include(e => e.DireccionPostal)
               .Include(e => e.Seguimientos)
               .FirstOrDefault(e => e.NroTracking == NroTracking);

            if (urgente != null)
                return urgente;

            var comun = _context.Comunes
                .Include(e => e.AgenciaOrigen)
                .Include(e => e.AgenciaDestino)
                .Include(e => e.Seguimientos)
                .FirstOrDefault(e => e.NroTracking == NroTracking);

            return comun;
        }

        public int Remove(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Envio obj)
        {

          //  var envio = _context.Envios.SingleOrDefault(e => e.Id == obj.Id);

            if (obj == null)
                throw new InvalidOperationException("Usuario no encontrado.");



            _context.SaveChanges();
            return obj.Id;
        }


    }
    }


