namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BITACORA_EVENTOS",
                c => new
                    {
                        IdEvento = c.Int(nullable: false, identity: true),
                        TablaDeEvento = c.String(nullable: false, maxLength: 20),
                        TipoDeEvento = c.String(nullable: false, maxLength: 20),
                        FechaDeEvento = c.DateTime(nullable: false),
                        DescripcionDeEvento = c.String(nullable: false),
                        StackTrace = c.String(),
                        DatosAnteriores = c.String(),
                        DatosPosteriores = c.String(),
                    })
                .PrimaryKey(t => t.IdEvento);
            
            CreateTable(
                "dbo.Comercio",
                c => new
                    {
                        IdComercio = c.Int(nullable: false, identity: true),
                        Identificacion = c.String(nullable: false, maxLength: 30),
                        TipoDeIdentificacionId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TipoDeComercioId = c.Int(nullable: false),
                        Telefono = c.String(nullable: false, maxLength: 20),
                        CorreoElectronico = c.String(nullable: false, maxLength: 200),
                        Direccion = c.String(nullable: false, maxLength: 500),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        FechaDeModificacion = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdComercio)
                .ForeignKey("dbo.TipoDeComercio", t => t.TipoDeComercioId, cascadeDelete: true)
                .ForeignKey("dbo.TipoDeIdentificacion", t => t.TipoDeIdentificacionId, cascadeDelete: true)
                .Index(t => t.TipoDeIdentificacionId)
                .Index(t => t.TipoDeComercioId);
            
            CreateTable(
                "dbo.TipoDeComercio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoDeIdentificacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sinpe",
                c => new
                    {
                        IdSinpe = c.Int(nullable: false, identity: true),
                        TelefonoOrigen = c.String(nullable: false, maxLength: 10),
                        NombreOrigen = c.String(nullable: false, maxLength: 200),
                        TelefonoDestinatario = c.String(nullable: false, maxLength: 10),
                        NombreDestinatario = c.String(nullable: false, maxLength: 200),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        Descripcion = c.String(maxLength: 50),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdSinpe);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comercio", "TipoDeIdentificacionId", "dbo.TipoDeIdentificacion");
            DropForeignKey("dbo.Comercio", "TipoDeComercioId", "dbo.TipoDeComercio");
            DropIndex("dbo.Comercio", new[] { "TipoDeComercioId" });
            DropIndex("dbo.Comercio", new[] { "TipoDeIdentificacionId" });
            DropTable("dbo.Sinpe");
            DropTable("dbo.TipoDeIdentificacion");
            DropTable("dbo.TipoDeComercio");
            DropTable("dbo.Comercio");
            DropTable("dbo.BITACORA_EVENTOS");
        }
    }
}
