using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using Spacer.Methods.Validacoes;
using Spacer.Models;

namespace Spacer.Controllers
{
    [Authorize(Roles = "Admin, CadastroPessoas")]
    public class PessoaFisicaController : Controller
    {
        private readonly ContextoDB _db = new ContextoDB();

        public ActionResult Index()
        {
            var model = _db.PF.ToList();

            return View(model);
        }

        public ActionResult Cadastro(int? id)
        {
            var model = new PF();

            if (id != null)
            {
                model = _db.PF.Find(id);
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastro(PF pf)
        {
            if (ModelState.IsValid)
            {
                if (pf.Id > 0)
                {
                    var pfOnDb = _db.PF.Find(pf.Id);

                    TryUpdateModel(pfOnDb);

                    _db.Entry(pfOnDb).State = EntityState.Modified;
                }
                else
                {
                    _db.PF.Add(pf);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(pf);
        }

        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = _db.PF.Find(id);

                if (model != null)
                {
                    _db.PF.Remove(model);
                    _db.SaveChanges();

                    return Json(new { excluiu = true });
                }
                else
                {
                    return Json(new { excluiu = false, msg = "Pessoa física não encontrada!" });
                }
            }
            else
            {
                return Json(new { excluiu = false, msg = "Pessoa física não encontrada!" });
            }
        }

        public ActionResult ValidarUsuarioUnico(string login)
        {
            var valido = true;

            if (!string.IsNullOrWhiteSpace(login.Trim()))
            {
                var pf = _db.PF.FirstOrDefault(f => f.Login == login);

                if (pf != null)
                {
                    valido = false;
                }

                if (valido)
                {
                    var pj = _db.PJ.FirstOrDefault(f => f.Login == login);

                    if (pj != null)
                    {
                        valido = false;
                    }
                }
            }

            return Json(valido, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidarCpf(string cpf)
        {
            var valido = true;

            if (cpf.Trim() != "___.___.___-__")
            {
                valido = Validacoes.ValidarCpf(cpf.Replace("_", ""));
            }

            return Json(valido, JsonRequestBehavior.AllowGet);
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