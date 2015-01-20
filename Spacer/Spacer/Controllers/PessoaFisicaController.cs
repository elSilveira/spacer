using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
                    _db.Entry(pf).State = EntityState.Modified;
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
    }
}