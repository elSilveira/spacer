using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Spacer.Models
{
    public class ContextoDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ContextoDB() : base("name=ContextoDB")
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<TipoEspaco> TipoEspaco { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // mapeamento de usuários e permissões
            modelBuilder.Entity<Usuario>()
                .HasMany(h => h.Permissoes)
                .WithMany(w => w.Usuarios)
                .Map(m =>
                {
                    m.ToTable("PermissaoUsuario");
                    m.MapLeftKey("UsuarioId");
                    m.MapRightKey("PermissaoId");
                });

            // ignora a propriedade ConfirmacaoSenha do modelo de usuário e faz
            // com que este campo não seja gerado no banco de dados
            modelBuilder.Entity<Usuario>().Ignore(i => i.ConfirmacaoSenha);

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Spacer.Models.UsuariosViewModel.AlteracaoSenhaViewModel> AlteracaoSenhaViewModels { get; set; }
    }
}
