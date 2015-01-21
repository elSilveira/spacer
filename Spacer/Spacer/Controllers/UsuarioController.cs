using System;
using System.Linq;
using System.Web.Mvc;
using Spacer.Methods.ClaimsMethods;
using Spacer.Models;
using Spacer.Models.UsuariosViewModel;

namespace Spacer.Controllers
{
    [Authorize(Roles = "Admin, CadastroUsuarios")]
    public class UsuarioController : Controller
    {
        private ContextoDB db = new ContextoDB();

        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        public ActionResult Cadastro(int? id)
        {
            var usuario = new Usuario();
            if (id != null && id > 0)
            {
                usuario = db.Usuario.Find(id);

                if (usuario == null)
                {
                    return HttpNotFound("Usuário não encontrado!");
                }
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "Id,Nome,Login,Senha,ConfirmacaoSenha")] Usuario usuario)
        {
            if (ModelState.IsValid || (usuario.Id > 0 && !string.IsNullOrWhiteSpace(usuario.Nome)))
            {
                if (usuario.Id > 0)
                {
                    // a única alteração que poderemos fazer no usuário é o nome dele
                    // a alteração da senha será em uma tela separada, específica
                    // este tbm é um exemplo de como alterar um registro usando SQL direto

                    var sql = string.Format("UPDATE Usuario SET Nome = '{0}' WHERE Id = {1}", usuario.Nome, usuario.Id);

                    db.Database.ExecuteSqlCommand(sql);
                }
                else
                {
                    db.Usuario.Add(usuario);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Permissoes(int? id)
        {
            Usuario user;

            if (id != null)
            {
                user = db.Usuario.Find(id);
            }
            else
            {
                var claimsManager = new ClaimsManager();

                user = claimsManager.GetClaimsIdentity(User.Identity);
            }

            ViewBag.Permissoes = db.Permissao.Where(w => w.Id > 1).ToList();

            return View(user);
        }

        [HttpPost]
        public ActionResult AlterarPermissao(int usuarioId, int permissaoId, bool incluir)
        {
            try
            {
                var sql = "";

                if (incluir)
                {
                    sql = string.Format("INSERT INTO PermissaoUsuario VALUES ({0}, {1})", usuarioId, permissaoId);
                }
                else
                {
                    sql = string.Format("DELETE FROM PermissaoUsuario WHERE UsuarioId = {0} AND PermissaoId = {1}",
                        usuarioId, permissaoId);
                }

                db.Database.ExecuteSqlCommand(sql);

                return Json(new { alterou = true });
            }
            catch (Exception ex)
            {
                var msg = string.IsNullOrWhiteSpace(ex.Message) ? ex.InnerException.Message : ex.Message;

                return Json(new { alterou = false, msg = msg });
            }
        }

        public ActionResult AlterarSenha(int id, bool meuPerfil = false)
        {
            var model = new AlteracaoSenhaViewModel
            {
                Id = id,
                Login = db.Usuario.Find(id).Login
            };

            ViewBag.MeuPerfil = meuPerfil;

            return View(model);
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlteracaoSenhaViewModel model, bool meuPerfil = false)
        {
            if (ModelState.IsValid)
            {
                var user =
                    db.Usuario.FirstOrDefault(
                        f => f.Id == model.Id && f.Login == model.Login && f.Senha == model.SenhaAtual);

                if (user != null)
                {
                    // a única alteração que poderemos fazer no usuário é o nome dele
                    // a alteração da senha será em uma tela separada, específica
                    // este tbm é um exemplo de como alterar um registro usando SQL direto

                    var sql = string.Format("UPDATE Usuario SET Senha = '{0}' WHERE Id = {1} AND Login = '{2}'",
                        model.NovaSenha, model.Id, model.Login);

                    db.Database.ExecuteSqlCommand(sql);


                    db.SaveChanges();

                    if (meuPerfil)
                    {
                        return RedirectToAction("MeuPerfil", "Acesso");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("SenhaAtual", "* Senha inválida! Digite a senha correta e tente novamente!");
                }
            }

            ViewBag.MeuPerfil = meuPerfil;

            return View(model);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return Json(new { excluiu = false, msg = "Usuário não encontrado" });
            }

            var usuario = db.Usuario.Find(id);

            if (usuario == null)
            {
                return Json(new { excluiu = false, msg = "Usuário não encontrado" });
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Json(new { excluiu = true });
        }

        public ActionResult ValidarUsuarioUnico(string login)
        {
            var valido = true;

            if (!string.IsNullOrWhiteSpace(login.Trim()))
            {
                var usuario = db.Usuario.FirstOrDefault(f => f.Login == login);

                if (usuario != null)
                {
                    valido = false;
                }
            }

            return Json(valido, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
