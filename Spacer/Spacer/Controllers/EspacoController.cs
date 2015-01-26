using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Cadastro(Espaco espaco, IEnumerable<HttpPostedFileBase> fotos)
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

                var fotosToSave = fotos as IList<HttpPostedFileBase> ?? fotos.ToList();

                for (var i = 0; i < fotosToSave.Count; i++)
                {
                    if (fotosToSave[i].ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(fotosToSave[i].FileName);

                        if (fileName != null)
                        {
                            var newFileName = string.Format("{0}-{1}", espaco.Id, fileName);
                            var path = Path.Combine(Server.MapPath("~/FotosEspacos"), newFileName);
                            fotosToSave[0].SaveAs(path);

                            switch (i)
                            {
                                default:
                                    break;
                                case 0:
                                    espaco.Foto01 = newFileName;
                                    break;
                                case 1:
                                    espaco.Foto02 = newFileName;
                                    break;
                                case 2:
                                    espaco.Foto03 = newFileName;
                                    break;
                            }
                        }
                    }
                }

                _db.Entry(espaco).State = EntityState.Modified;

                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            if (espaco.Id > 0)
            {   
                ViewBag.Foto1 = espaco.Foto01;
                ViewBag.Foto2 = espaco.Foto02;
                ViewBag.Foto3 = espaco.Foto03;
            }

            ViewBag.TipoEspaco = new SelectList(_db.TipoEspaco, "Id", "Nome", espaco.TipoEspacoId);

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

        public ActionResult CarregarValorEspaco(int id)
        {
            var valor = _db.Espaco.Find(id).Valor.ToString("N2");

            return Json(valor, JsonRequestBehavior.AllowGet);
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