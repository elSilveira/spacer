namespace Spacer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Usuario", "Login", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Senha", c => c.String());
            AlterColumn("dbo.Usuario", "Login", c => c.String());
            AlterColumn("dbo.Usuario", "Nome", c => c.String());
        }
    }
}
