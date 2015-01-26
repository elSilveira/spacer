using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spacer.Models;

namespace Spacer.Controllers
{
    public class TipoEspacoController : Controller
    {
        private ContextoDB db = new ContextoDB();

        [Authorize(Roles = "Admin, VisualizacaoTipoEspaco, CadastroTipoEspaco")]
        public ActionResult Index()
        {
            var model = db.TipoEspaco.ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroTipoEspaco")]
        public ActionResult Cadastro(int? id)
        {
            var model = new TipoEspaco();

            if (id != null && id > 0)
            {
                model = db.TipoEspaco.Find(id);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroTipoEspaco")]
        [HttpPost]
        public ActionResult Cadastro(TipoEspaco tipoEspaco)
        {
            if (ModelState.IsValid)
            {
                if (tipoEspaco.Id > 0)
                {
                    db.Entry(tipoEspaco).State = EntityState.Modified;
                }
                else
                {
                    db.TipoEspaco.Add(tipoEspaco);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tipoEspaco);
        }

        [Authorize(Roles = "Admin, CadastroTipoEspaco")]
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = db.TipoEspaco.Find(id);

                if (model != null)
                {
                    db.TipoEspaco.Remove(model);
                    db.SaveChanges();

                    return Json(new {excluiu = true});
                }
                else
                {
                    return Json(new { excluiu = false, msg = "Tipo espaço não encontrado!" });
                }
            }
            else
            {
                return Json(new {excluiu = false, msg = "Tipo espaço não encontrado!"});
            }
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