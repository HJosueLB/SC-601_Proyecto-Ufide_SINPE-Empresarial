using SINPE.Empresarial.Domain.BitacoraDomain.Entities;
using SINPE.Empresarial.Domain.CajaDomain.Entities;
using SINPE.Empresarial.Domain.ComercioDomain.Entities;
using SINPE.Empresarial.Domain.ConfiguracionDomain.Entities;
using SINPE.Empresarial.Domain.ReporteDomain.Entities;
using SINPE.Empresarial.Domain.SinpeDomain.Entities;
using SINPE.Empresarial.Domain.UsuarioDomain.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace SINPE.Empresarial.Infrastructure.Data
{
    public class SINPE_Empresarial_DB : DbContext
    {
        public SINPE_Empresarial_DB(string connectionString)
            :base(connectionString)
        {
        }
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

        // DbSet: Mapear la tabla de la entidad Caja
        public DbSet<Caja> Cajas { get; set; }

        // DbSet: Mapear la tabla de la entidad Sinpe
        public DbSet<Sinpe> Sinpe { get; set; }

        // DbSet: Mapear la tabla de la entidad BitacoraEvento
        public DbSet<BitacoraEvento> BitacoraEventos { get; set; }

        // DbSet: Mapear la tabla de la entidad Configuracion
        public DbSet<Configuracion> Configuraciones { get; set; }

        // DbSet: Mapear la tabla de la entidad ReporteMensual
        public DbSet<ReporteMensual> ReportesMensuales { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comercio>().ToTable("Comercio");
            modelBuilder.Entity<TipoDeComercio>().ToTable("TipoDeComercio");
            modelBuilder.Entity<TipoDeIdentificacion>().ToTable("TipoDeIdentificacion");
            modelBuilder.Entity<Sinpe>().ToTable("Sinpe");

            // Mapeo de la entidad BitacoraEvento a la tabla BITACORA_EVENTOS
            modelBuilder.Entity<BitacoraEvento>().ToTable("BITACORA_EVENTOS");

            modelBuilder.Entity<Configuracion>().ToTable("Configuraciones");
            modelBuilder.Entity<ReporteMensual>().ToTable("ReportesMensuales");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}