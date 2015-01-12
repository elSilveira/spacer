using System.Collections.Generic;

namespace Spacer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Spacer.Models;

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
            context.Permissao.AddOrUpdate(
              p => p.Chave,
              new Permissao { Chave = "Admin", Nome = "Administração do Sistema"},
              new Permissao { Chave = "CadastroUsuarios", Nome = "Controle de Usuários" },
              new Permissao { Chave = "CadastroPermissoes", Nome = "Controle de Permissões" }
            );

            context.Usuario.AddOrUpdate(
              p => p.Nome,
              new Usuario { Nome = "Andre", Login = "andre", Senha = "123456"}
              
            );
            //
        }
    }
}
