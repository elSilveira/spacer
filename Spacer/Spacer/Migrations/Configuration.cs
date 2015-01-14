namespace Spacer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Spacer.Models.ContextoDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Spacer.Models.ContextoDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            // Você pode usar o Seed como a seguir
            //context.Permissao.AddOrUpdate(
            //  p => p.Chave,
            //  new Permissao { Chave = "Admin", Nome = "Administração do Sistema"},
            //  new Permissao { Chave = "CadastroUsuarios", Nome = "Controle de Usuários" },
            //  new Permissao { Chave = "CadastroPermissoes", Nome = "Controle de Permissões" }
            //);

            //context.Usuario.AddOrUpdate(
            //  p => p.Nome,
            //  new Usuario { Nome = "Andre", Login = "andre", Senha = "123456"}

            //);
            //

            // ou pode usar SQL diretamente, como à seguir

            const string sql = @"
                    IF NOT EXISTS (SELECT Id FROM Permissao WHERE Id = 12)
                    BEGIN
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Administração Geral do Sistema', 'Admin') -- 1
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Usuários', 'CadastroUsuarios') -- 2
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Alteração de Permissoes', 'AlteracaoPermissoes') -- 3
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Clientes', 'CadastroPessoas') -- 4
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Espaços', 'CadastroEspacos') -- 5
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Agendamentos', 'CadastroAgendamentos') -- 6
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Tipos de Espaços', 'CadastroTipoEspaco') -- 7
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Visualização de Tipos de Espaços', 'VisualizacaoTipoEspaco') -- 8
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Formas de Pagamentos', 'CadastroFormasPagamentos') -- 9
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Visualização de Formas de Pagamentos', 'VisualizacaoFormasPagamentos') -- 10
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Cadastro de Avaliações', 'CadastroAvaliaçoes') -- 11
	                    INSERT INTO Permissao ( Nome, Chave) VALUES ('Visualização de Avaliações', 'VisualizacaoAvaliacoes') -- 12
                    END
                ";
            context.Database.ExecuteSqlCommand(sql);
        }
    }
}
