using SINPE_Empresarial.Domain.ComercioDomain.Entities;
using System;
using System.Data.Entity;
using System.Linq;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comercio>().ToTable("Comercio");
            modelBuilder.Entity<TipoDeComercio>().ToTable("TipoDeComercio");
            modelBuilder.Entity<TipoDeIdentificacion>().ToTable("TipoDeIdentificacion");
        }

    }


}