using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spacer.Methods.Validacoes;
using Spacer.Models;

namespace Spacer.Controllers
{
    [Authorize(Roles = "Admin, CadastroPessoas")]
    public class PessoaJuridicaController : Controller
    {
        private readonly ContextoDB _db = new ContextoDB();

        public ActionResult Index()
        {
            var model = _db.PJ.ToList();

            return View(model);
        }

        public ActionResult Cadastro(int? id)
        {
            var model = new PJ();

            if (id != null)
            {
                model = _db.PJ.Find(id);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Cadastro(PJ PJ)
        {
            if (ModelState.IsValid)
            {
                if (PJ.Id > 0)
                {
                    _db.Entry(PJ).State = EntityState.Modified;
                }
                else
                {
                    _db.PJ.Add(PJ);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(PJ);
        }

        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = _db.PJ.Find(id);

                if (model != null)
                {
                    _db.PJ.Remove(model);
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

        public ActionResult ValidarCnpj(string cnpj)
        {
            var valido = true;

            if (cnpj.Trim() != "______________")
            {
                valido = Validacoes.ValidarCnpj(cnpj.Replace("_", ""));
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