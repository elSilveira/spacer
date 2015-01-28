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
using Spacer.Methods.ClaimsMethods;

namespace Spacer.Controllers
{
    public class AcessoController : Controller
    {
        readonly ContextoDB _db = new ContextoDB();
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        readonly ClaimsManager _claimsManager = new ClaimsManager();

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
                    var user = new Usuario();

                    if (model.Login.ToUpper() == "ADMIN" && model.Senha.ToUpper() == "123456")
                    {
                        user.Id = -1;
                        user.Nome = "Administrador do Sistema";
                        user.Login = "Administrador";
                    }
                    else
                    {
                        user = _db.Usuario.FirstOrDefault(f =>
                        f.Login == model.Login && f.Senha == model.Senha);
                    }
                    
                    if (user == null)
                    {
                        ModelState.AddModelError("Login",
                            "* Nome de usuário ou senha incorretos! Por favor verifique seus dados e tente novamente.");
                        return View(model);
                    }

                    user.TipoCliente = 0;

                    // caso precise ter o ip, essa é uma das formas que podem ser usadas
                    //var ip = string.IsNullOrWhiteSpace(Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                    //    ? Request.ServerVariables["REMOTE_ADDR"]
                    //    : Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    _claimsManager.SignIn(AuthenticationManager, user, model.RememberMe);

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
            _claimsManager.LogOff(AuthenticationManager);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult MeuPerfil()
        {
            //pega as informações de acordo com o usuário logado
            var user = _claimsManager.GetClaimsIdentity(User.Identity);

            if (!User.IsInRole("Admin"))
            {
                ViewBag.Permissoes = _db.Permissao.Where(w => w.Id > 1).ToList();
            }

            return View(user);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}