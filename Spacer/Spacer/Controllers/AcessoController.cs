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

namespace Spacer.Controllers
{
    public class AcessoController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Index(LoginViewModel model, string returnUrl)
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var user = await ValidateLogin(model.Login, model.Senha);

                    //MOCK PARA TESTAR USUARIO
                    Usuario user = null;

                    if (model.Login.ToUpper() == "ADMIN" && model.Senha.ToUpper() == "123456")
                    {
                        user = new Usuario {Id = -1, Nome = "Administrador do Sistema", Login = "Admin"};
                    }

                    if (user == null)
                    {
                        ModelState.AddModelError("Login",
                            "* Nome de usuário ou senha incorretos! Por favor verifique seus dados e tente novamente.");
                        return View(model);
                    }

                    SignIn(user, model.RememberMe);

                    return RedirectToLocal(returnUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Login", ex.Message);
                }
            }

            return View(model);
        }
        
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MinhaConta()
        {
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SignIn(Usuario user, bool rememberMe)
        {
            try
            {
                AuthenticationManager.SignOut("SpacerApplication");

                var ip = string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                    ? Request.ServerVariables["REMOTE_ADDR"]
                    : Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                // Criação da instancia do Identity e atribuicao dos claims
                var claimsIdentity = SetClaimsIdentity(user);

                AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = rememberMe}, claimsIdentity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ClaimsIdentity SetClaimsIdentity(Usuario user)
        {
            var id = new Claim("Id", user.Id.ToString(), "string", "LOCAL AUTHORITY", ClaimsIdentity.DefaultIssuer);
            var nome = new Claim(ClaimTypes.Name, user.Nome);
            var login = new Claim("Login", user.Login);
            var nameidentifier = new Claim(ClaimTypes.NameIdentifier, new Guid().ToString());
            var identityProvider = new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", new Guid().ToString());

            var claims = new List<Claim> { id, nome, login, nameidentifier, identityProvider };

            // adiciona as permissões deste usuário
            //claims.AddRange(user.Permissoes.ToList().Select(perm => new Claim(ClaimTypes.Role, perm.Chave)));

            //var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie");
            var claimsIdentity = new ClaimsIdentity(claims, "SpacerApplication");

            return claimsIdentity;
        }

        public Usuario GetClaimsIdentity(IIdentity userPrincipal)
        {
            var identity = (ClaimsIdentity)userPrincipal;

            var id = identity.FindFirst("Id");
            var nome = identity.FindFirst(ClaimTypes.Name);
            var login = identity.FindFirst("Login");

            var user = new Usuario
            {
                Id = Convert.ToInt32(id),
                Nome = nome.ToString(),
                Login = login.ToString()
            };

            return user;
        }
    }
}