using AgenciaEnvios.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgenciaEnvios.LogicaAccesoDatos.Migrations
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {




        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Comun> Comunes { get; set; }
        public DbSet<Urgente> Urgentes { get; set; }
        public DbSet<Seguimiento> seguimientos { get; set; }
        public DbSet<Agencia> Agencias { get; set; }





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

            modelBuilder.Entity<Envio>()
                   .HasDiscriminator<string>("Tipo")
                   .HasValue<Envio>("Envio")
                   .HasValue<Comun>("Comun")
                   .HasValue<Urgente>("Urgente");


            modelBuilder.Entity<Envio>()
                .HasOne(e => e.AgenciaOrigen)
                .WithMany()
                .HasForeignKey(e => e.AgenciaOrigenId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Comun>()
                .HasOne(c => c.AgenciaDestino)
                .WithMany()
                .HasForeignKey(c => c.AgenciaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);











            modelBuilder.Entity<Urgente>()
         .OwnsOne(u => u.DireccionPostal, dp =>
         {
             dp.Property(e => e.Destinatario).HasColumnName("Destinatario");
             dp.Property(d => d.Calle).HasColumnName("Calle");
             dp.Property(d => d.Numero).HasColumnName("Numero");
             dp.Property(d => d.Ciudad).HasColumnName("Ciudad");
             dp.Property(d => d.CodigoPostal).HasColumnName("CodigoPostal");
             dp.Property(d => d.Departamento).HasColumnName("Departamento");
         });


            modelBuilder.Entity<Agencia>()
           .OwnsOne(a => a.DireccionPostal, dp =>
           {
               dp.Property(d => d.Destinatario).HasColumnName("Destinatario");
               dp.Property(d => d.Calle).HasColumnName("Calle");
               dp.Property(d => d.Numero).HasColumnName("Numero");
               dp.Property(d => d.Ciudad).HasColumnName("Ciudad");
               dp.Property(d => d.CodigoPostal).HasColumnName("CodigoPostal");
               dp.Property(d => d.Departamento).HasColumnName("Departamento");
           });




           
        }
    }










}


