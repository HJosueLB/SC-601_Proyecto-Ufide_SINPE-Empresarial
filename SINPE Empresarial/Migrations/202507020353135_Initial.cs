namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BITACORA_EVENTOS");
        }
    }
}
