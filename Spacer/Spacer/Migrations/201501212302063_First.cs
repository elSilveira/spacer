namespace Spacer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agendamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataAgendamento = c.DateTime(nullable: false),
                        ValorAgendamento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorPago = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Periodo = c.Int(nullable: false),
                        EspacoId = c.Int(nullable: false),
                        FormaPagamentoId = c.Int(nullable: false),
                        PFId = c.Int(nullable: false),
                        PJId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PF", t => t.PFId, cascadeDelete: true)
                .ForeignKey("dbo.PJ", t => t.PJId, cascadeDelete: true)
                .Index(t => t.PFId)
                .Index(t => t.PJId);
            
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
                "dbo.Espaco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        TipoEspacoId = c.Int(nullable: false),
                        Capacidade = c.Int(nullable: false),
                        Foto01 = c.String(),
                        Foto02 = c.String(),
                        Foto03 = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoEspaco", t => t.TipoEspacoId, cascadeDelete: true)
                .Index(t => t.TipoEspacoId);
            
            CreateTable(
                "dbo.TipoEspaco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FormaPagamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
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
                "dbo.PF",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 200),
                        DataNascimento = c.DateTime(nullable: false),
                        CPF = c.String(nullable: false, maxLength: 14),
                        RG = c.String(maxLength: 20),
                        Genero = c.Int(nullable: false),
                        Endereco = c.String(nullable: false, maxLength: 300),
                        Bairro = c.String(maxLength: 50),
                        CEP = c.String(nullable: false, maxLength: 9),
                        FonePrincipal = c.String(nullable: false, maxLength: 15),
                        FoneSecundario = c.String(maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 100),
                        Login = c.String(nullable: false, maxLength: 20),
                        Senha = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PJ",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeFantasia = c.String(nullable: false, maxLength: 200),
                        RazaoSocial = c.String(nullable: false, maxLength: 200),
                        DataAbertura = c.DateTime(nullable: false),
                        CNPJ = c.String(nullable: false, maxLength: 18),
                        InscricaoEstadual = c.String(nullable: false, maxLength: 15),
                        Endereco = c.String(nullable: false, maxLength: 300),
                        Bairro = c.String(maxLength: 50),
                        CEP = c.String(nullable: false, maxLength: 9),
                        FonePrincipal = c.String(nullable: false, maxLength: 15),
                        FoneSecundario = c.String(maxLength: 15),
                        Email = c.String(nullable: false, maxLength: 100),
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
            DropForeignKey("dbo.Agendamento", "PJId", "dbo.PJ");
            DropForeignKey("dbo.Agendamento", "PFId", "dbo.PF");
            DropForeignKey("dbo.PermissaoUsuario", "PermissaoId", "dbo.Permissao");
            DropForeignKey("dbo.PermissaoUsuario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Espaco", "TipoEspacoId", "dbo.TipoEspaco");
            DropIndex("dbo.PermissaoUsuario", new[] { "PermissaoId" });
            DropIndex("dbo.PermissaoUsuario", new[] { "UsuarioId" });
            DropIndex("dbo.Espaco", new[] { "TipoEspacoId" });
            DropIndex("dbo.Agendamento", new[] { "PJId" });
            DropIndex("dbo.Agendamento", new[] { "PFId" });
            DropTable("dbo.PermissaoUsuario");
            DropTable("dbo.PJ");
            DropTable("dbo.PF");
            DropTable("dbo.Usuario");
            DropTable("dbo.Permissao");
            DropTable("dbo.FormaPagamento");
            DropTable("dbo.TipoEspaco");
            DropTable("dbo.Espaco");
            DropTable("dbo.AlteracaoSenhaViewModel");
            DropTable("dbo.Agendamento");
        }
    }
}
