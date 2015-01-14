namespace Spacer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlteracaoSenhaViewModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        SenhaAtual = c.String(),
                        NovaSenha = c.String(nullable: false, maxLength: 50),
                        ConfirmacaoNovaSenha = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Chave = c.String(),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200),
                        Login = c.String(nullable: false, maxLength: 20),
                        Senha = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissaoUsuario",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false),
                        PermissaoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioId, t.PermissaoId })
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .ForeignKey("dbo.Permissao", t => t.PermissaoId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.PermissaoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissaoUsuario", "PermissaoId", "dbo.Permissao");
            DropForeignKey("dbo.PermissaoUsuario", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.PermissaoUsuario", new[] { "PermissaoId" });
            DropIndex("dbo.PermissaoUsuario", new[] { "UsuarioId" });
            DropTable("dbo.PermissaoUsuario");
            DropTable("dbo.Usuario");
            DropTable("dbo.Permissao");
            DropTable("dbo.AlteracaoSenhaViewModel");
        }
    }
}
