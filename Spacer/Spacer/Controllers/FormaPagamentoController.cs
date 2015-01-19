using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spacer.Models;

namespace Spacer.Controllers
{
    public class FormaPagamentoController : Controller
    {
        private ContextoDB db = new ContextoDB();

        [Authorize(Roles = "Admin, VisualizacaoFormaPagamento, CadastroFormaPagamento")]
        public ActionResult Index()
        {
            var model = db.FormaPagamento.ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroFormaPagamento")]
        public ActionResult Cadastro(int? id)
        {
            var model = new FormaPagamento();

            if (id != null && id > 0)
            {
                model = db.FormaPagamento.Find(id);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroFormaPagamento")]
        [HttpPost]
        public ActionResult Cadastro(FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                if (formaPagamento.Id > 0)
                {
                    db.Entry(formaPagamento).State = EntityState.Modified;
                }
                else
                {
                    db.FormaPagamento.Add(formaPagamento);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(formaPagamento);
        }

        [Authorize(Roles = "Admin, CadastroFormaPagamento")]
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = db.FormaPagamento.Find(id);

                if (model != null)
                {
                    db.FormaPagamento.Remove(model);
                    db.SaveChanges();

                    return Json(new { excluiu = true });
                }
                else
                {
                    return Json(new { excluiu = false, msg = "Forma de pagamento não encontrada!" });
                }
            }
            else
            {
                return Json(new { excluiu = false, msg = "Forma de pagamento não encontrada!" });
            }
        }
    }
}