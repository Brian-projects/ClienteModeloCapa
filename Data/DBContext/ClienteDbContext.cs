using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Data.Models;
namespace Data.DBContext
{
    public class ClienteDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoCliente>TipoClientes { get; set; }
        public DbSet<Estatus> Estatus { get; set; }
        public ClienteDbContext() : base("DefaultConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoCliente>()
                .HasRequired(x => x.Estatus)
                .WithMany(x => x.tipoClientes)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
