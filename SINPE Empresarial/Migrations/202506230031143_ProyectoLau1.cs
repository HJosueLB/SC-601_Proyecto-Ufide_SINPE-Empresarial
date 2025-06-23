namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProyectoLau1 : DbMigration
    {
        public override void Up()
        {
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
            DropTable("dbo.Sinpe");
        }
    }
}
