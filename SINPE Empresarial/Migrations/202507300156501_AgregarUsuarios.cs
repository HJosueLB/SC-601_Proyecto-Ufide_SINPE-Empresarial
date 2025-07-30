namespace SINPE_Empresarial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarUsuarios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        IdComercio = c.Int(nullable: false),
                        IdNetUser = c.Guid(),
                        Nombres = c.String(nullable: false, maxLength: 100),
                        PrimerApellido = c.String(nullable: false, maxLength: 100),
                        SegundoApellido = c.String(nullable: false, maxLength: 100),
                        Identificacion = c.String(nullable: false, maxLength: 10),
                        CorreoElectronico = c.String(nullable: false, maxLength: 200),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        FechaDeModificacion = c.DateTime(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .ForeignKey("dbo.Comercio", t => t.IdComercio, cascadeDelete: true)
                .Index(t => t.IdComercio);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "IdComercio", "dbo.Comercio");
            DropIndex("dbo.Usuario", new[] { "IdComercio" });
            DropTable("dbo.Usuario");
        }
    }
}
