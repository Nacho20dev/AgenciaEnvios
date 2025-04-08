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

    }

}


