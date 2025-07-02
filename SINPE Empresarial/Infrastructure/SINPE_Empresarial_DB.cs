using SINPE_Empresarial.Domain.BitacoraDomain.Entities; // Asegúrate de que esta línea esté presente
using SINPE_Empresarial.Domain.CajaDomain.Entities;
using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using SINPE_Empresarial.Domain.SinpeDomain.Entities;
using System.Data.Entity;

namespace SINPE_Empresarial
{
    public class SINPE_Empresarial_DB : DbContext
    {
        public SINPE_Empresarial_DB()
            : base("name=SINPE_Empresarial_DB")
        {
        }

        // DbSet: Mapea la entidad Comercio a la tabla 'Comercio'
        public DbSet<Comercio> Comercio { get; set; }

        // DbSet: Mapear la tabla de la entidad TipoDeIdentificacion
        public DbSet<TipoDeIdentificacion> TipoDeIdentificacion { get; set; }

        // DbSet: Mapear la tabla de la entidad TipoDeComercio
        public DbSet<TipoDeComercio> TipoDeComercio { get; set; }

        // DbSet: Mapear la tabla de la entidad Sinpe
        public DbSet<Sinpe> Sinpe { get; set; }

        // DbSet: Mapear la tabla de la entidad BitacoraEvento
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; } // Nueva línea añadida

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comercio>().ToTable("Comercio");
            modelBuilder.Entity<TipoDeComercio>().ToTable("TipoDeComercio");
            modelBuilder.Entity<TipoDeIdentificacion>().ToTable("TipoDeIdentificacion");
            modelBuilder.Entity<Sinpe>().ToTable("Sinpe");

            // Mapeo de la entidad BitacoraEvento a la tabla BITACORA_EVENTOS
            modelBuilder.Entity<BitacoraEvento>().ToTable("BITACORA_EVENTOS");
        }
    }
}
