namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReporteMensual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportesMensuales",
                c => new
                    {
                        IdReporte = c.Int(nullable: false, identity: true),
                        IdComercio = c.Int(nullable: false),
                        CantidadDeCajas = c.Int(nullable: false),
                        MontoTotalRecaudado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantidadDeSINPES = c.Int(nullable: false),
                        MontoTotalComision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaDelReporte = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdReporte)
                .ForeignKey("dbo.Comercio", t => t.IdComercio, cascadeDelete: true)
                .Index(t => t.IdComercio);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportesMensuales", "IdComercio", "dbo.Comercio");
            DropIndex("dbo.ReportesMensuales", new[] { "IdComercio" });
            DropTable("dbo.ReportesMensuales");
        }
    }
}
