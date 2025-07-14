namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarConfiguracionesTabla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configuraciones",
                c => new
                {
                    IdConfiguracion = c.Int(nullable: false, identity: true),
                    IdComercio = c.Int(nullable: false),
                    TipoConfiguracion = c.Int(nullable: false),
                    Comision = c.Int(nullable: false),
                    FechaDeRegistro = c.DateTime(nullable: false),
                    FechaDeModificacion = c.DateTime(),
                    Estado = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.IdConfiguracion)
                .ForeignKey("dbo.Comercio", t => t.IdComercio, cascadeDelete: false)
                .Index(t => t.IdComercio);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Configuraciones", "IdComercio", "dbo.Comercio");
            DropIndex("dbo.Configuraciones", new[] { "IdComercio" });
            DropTable("dbo.Configuraciones");
        }
    }
}
