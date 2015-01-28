using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Spacer.Methods.ClaimsMethods;
using Spacer.Models;

namespace Spacer.Providers
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            try
            {
                var db = new ContextoDB();

                var userName = context.UserName;
                var password = context.Password;

                var usuario = new Usuario();
                
                var usuariopf = await db.PF.FirstOrDefaultAsync(f => f.Login == userName && f.Senha == password);

                if (usuariopf != null)
                {
                    usuario.Id = usuariopf.Id;
                    usuario.Nome = usuariopf.Nome;
                    usuario.Login = usuariopf.Login;
                    usuario.TipoCliente = 1;
                }
                else
                {
                    var usuariopj = await db.PJ.FirstOrDefaultAsync(f => f.Login == userName && f.Senha == password);

                    if (usuariopj != null)
                    {
                        usuario.Id = usuariopj.Id;
                        usuario.Nome = usuariopj.NomeFantasia;
                        usuario.Login = usuariopj.Login;
                        usuario.TipoCliente = 2;
                    }
                }

                if (usuario != new Usuario())
                {
                    var permissoes = new List<Permissao>
                    {
                        new Permissao {Nome = "Visualização de Tipos de Espaços", Chave = "VisualizacaoTipoEspaco"},
                        new Permissao {Nome = "Visualização de Formas de Pagamentos", Chave = "VisualizacaoFormasPagamentos"},
                        new Permissao {Nome = "Cadastro de Avaliações", Chave = "VisualizacaoAvaliacoes"},
                        new Permissao {Nome = "Cadastro de Agendamentos", Chave = "CadastroAgendamentos"}
                    };
                    
                    usuario.Permissoes = permissoes;

                    var claimsManager = new ClaimsManager();

                    var identity = claimsManager.SetClaimsIdentity(usuario);

                    var principal = new GenericPrincipal(identity, permissoes.Select(s => s.Chave).ToArray());
                    Thread.CurrentPrincipal = principal;

                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Falha na autenticação");
                }
            }
            catch
            {
                context.SetError("invalid_grant", "Falha na autenticação");
            }
        }
    }
}