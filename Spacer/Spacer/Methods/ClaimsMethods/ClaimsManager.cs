using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Spacer.Models.UsuariosViewModel;
using Spacer.Models;
using System.Security.Principal;
using System.Web.WebSockets;
using Spacer.Methods.ClaimsMethods;

namespace Spacer.Methods.ClaimsMethods
{
    public class ClaimsManager : Controller
    {
        #region Authorization
        /// <summary>
        /// Faz a autenticação de acordo com o login e senha passados (caso estejam corretos)
        /// </summary>
        /// <param name="authenticationManager">IAuthenticationManager</param>
        /// <param name="user">Usuario</param>
        /// <param name="rememberMe">bool</param>
        internal void SignIn(IAuthenticationManager authenticationManager, Usuario user, bool rememberMe)
        {
            try
            {
                authenticationManager.SignOut("SpacerApplication");

                // Criação da instancia do Identity e atribuicao dos claims
                var claimsIdentity = SetClaimsIdentity(user);

                authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, claimsIdentity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Faz o logoff, terminando a sessão do usuário
        /// </summary>
        /// <param name="authenticationManager">IAuthenticationManager</param>
        internal void LogOff(IAuthenticationManager authenticationManager)
        {
            authenticationManager.SignOut();
        } 
        #endregion

        #region Get/Set Claims
        /// <summary>
        /// Retorna um ClaimsIdentity preenchido com os dados do usuário para fazer o login
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <returns>ClaimsIdentity</returns>
        internal ClaimsIdentity SetClaimsIdentity(Usuario user)
        {
            var id = new Claim("Id", user.Id.ToString(), "string", "LOCAL AUTHORITY", ClaimsIdentity.DefaultIssuer);
            var nome = new Claim(ClaimTypes.Name, user.Nome);
            var login = new Claim("Login", user.Login);
            var tipoCliente = new Claim("TipoCliente", user.TipoCliente.ToString());
            var nameidentifier = new Claim(ClaimTypes.NameIdentifier, new Guid().ToString());
            var identityProvider = new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", new Guid().ToString());

            var claims = new List<Claim> { id, nome, login, tipoCliente, nameidentifier, identityProvider };

            // adiciona as permissões deste usuário
            // se for diferente de -1, é um usuário comum, sendo
            // carregadas apenas as permissões do próprio usuário
            if (user.Id != -1) 
            {
                claims.AddRange(user.Permissoes.ToList().Select(perm => new Claim(ClaimTypes.Role, perm.Chave)));
            }
            // se for igual -1, é o Administrador do Sistema, 
            // portanto, carregamos a permissão mais importante 
            // e que dá acesso ao sistema inteiro: Admin
            else 
            {
                var db = new ContextoDB();
                claims.Add(new Claim(ClaimTypes.Role, db.Permissao.Single(f => f.Id == 1).Chave));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "SpacerApplication");

            return claimsIdentity;
        }
        
        /// <summary>
        /// Retorna um Usuário de acordo com a Identity da sessão
        /// </summary>
        /// <param name="userPrincipal">IIdentity</param>
        /// <returns>Usuario</returns>
        internal Usuario GetClaimsIdentity(IIdentity userPrincipal)
        {
            var identity = (ClaimsIdentity)userPrincipal;

            var id = identity.FindFirst("Id").Value;
            var nome = identity.FindFirst(ClaimTypes.Name).Value;
            var login = identity.FindFirst("Login").Value;
            var tipoCliente = identity.FindFirst("TipoCliente").Value;
            var roles = identity.FindAll(f => f.Type == ClaimTypes.Role).ToList();

            var db = new ContextoDB();

            var permissoes = roles.Select(r => db.Permissao.Single(s => s.Chave == r.Value)).ToList();

            var user = new Usuario
            {
                Id = Convert.ToInt32(id),
                Nome = nome.ToString(),
                Login = login.ToString(),
                Permissoes = permissoes,
                TipoCliente = Convert.ToInt32(tipoCliente)
            };

            return user;
        } 
        #endregion
    }
}