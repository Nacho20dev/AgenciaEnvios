using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {


        
        
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public  DbSet<Auditoria> Auditorias { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{




        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(
        //    @"Server=DESKTOP-7E9VS64\SQL;Database=Agencia_Envios;Integrated Security=True;");

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuración de la entidad usuario
            modelBuilder.Entity<Usuario>()

                .HasIndex(c => c.Email)
                .IsUnique(); // Crea un índice único para el campo Email

        }




    }
}


