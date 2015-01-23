using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spacer.Models;

namespace Spacer.Controllers
{
    public class EspacoController : Controller
    {
        private readonly ContextoDB _db = new ContextoDB();

        [Authorize(Roles = "Admin, VisualizacaoEspaco, CadastroEspaco")]
        public ActionResult Index()
        {
            var model = _db.Espaco.ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroEspaco")]
        public ActionResult Cadastro(int? id)
        {
            var model = new Espaco();

            if (id != null)
            {
                model = _db.Espaco.Find(id);
                ViewBag.Foto1 = model.Foto01;
                ViewBag.Foto2 = model.Foto02;
                ViewBag.Foto3 = model.Foto03;
            }

            ViewBag.TipoEspaco = new SelectList(_db.TipoEspaco, "Id", "Nome", model.TipoEspacoId);

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroEspaco")]
        [HttpPost]
        public ActionResult Cadastro(Espaco espaco)
        {
            if (ModelState.IsValid)
            {
                if (espaco.Id > 0)
                {
                    _db.Entry(espaco).State = EntityState.Modified;
                }
                else
                {
                    _db.Espaco.Add(espaco);
                }

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(espaco);
        }

        [Authorize(Roles = "Admin, CadastroEspaco")]
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = _db.Espaco.Find(id);

                if (model != null)
                {
                    _db.Espaco.Remove(model);
                    _db.SaveChanges();

                    return Json(new { excluiu = true });
                }
                else
                {
                    return Json(new { excluiu = false, msg = "Espaço não encontrado!" });
                }
            }
            else
            {
                return Json(new { excluiu = false, msg = "Espaço não encontrado!" });
            }
        }

    }
}