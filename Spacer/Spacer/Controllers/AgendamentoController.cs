using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Spacer.Models;

namespace Spacer.Controllers
{
    public class AgendamentoController : Controller
    {
        private ContextoDB db = new ContextoDB();

        [Authorize(Roles = "Admin, VisualizacaoAgendamento, CadastroAgendamento")]
        public ActionResult Index(int? id, int? tipoCliente)
        {
            List<Agendamento> model;

            if (id != null && tipoCliente != null)
            {
                if (tipoCliente == 1)
                {
                    model = db.Agendamento
                        .Where(w => w.PFId == id)
                        .ToList();
                }
                else
                {
                    model = db.Agendamento
                        .Where(w => w.PJId == id)
                        .ToList();
                }
            }
            else
            {
                model = db.Agendamento.ToList();
            }

            ViewBag.Id = id;
            ViewBag.TipoCliente = tipoCliente;

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroAgendamento")]
        public ActionResult Cadastro(int? id, int? clientid, int? tipoCliente)
        {
            var model = new Agendamento();

            if (id != null && id > 0)
            {
                model = db.Agendamento.Find(id);
            }
            else if (clientid != null && tipoCliente != null)
            {
                if (tipoCliente == 1)
                {
                    model.PFId = clientid.Value;
                    model.PF = db.PF.Find(clientid);
                }
                else
                {
                    model.PJId = clientid.Value;
                    model.PJ = db.PJ.Find(clientid);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Por favor, informe o cliente!");
            }

            ViewBag.EspacoId = new SelectList(db.Espaco.ToList(), "Id", "Nome", model.EspacoId);
            ViewBag.FormaPagamentoId = new SelectList(db.FormaPagamento.ToList(), "Id", "Nome", model.FormaPagamentoId);

            ViewBag.ClientId = clientid;
            ViewBag.TipoCliente = tipoCliente;

            ViewBag.PossuiAvaliacao =
                db.AvaliacaoEspaco.Count(
                    c => c.EspacoId == model.EspacoId && (c.PFId == model.PFId || c.PJId == model.PJId)) > 0;

            return View(model);
        }

        [Authorize(Roles = "Admin, CadastroAgendamento")]
        [HttpPost]
        public ActionResult Cadastro(Agendamento agendamento, int tipoCliente)
        {
            if (ModelState.IsValid)
            {
                if (agendamento.Id > 0)
                {
                    db.Entry(agendamento).State = EntityState.Modified;
                }
                else
                {
                    db.Agendamento.Add(agendamento);
                }

                db.SaveChanges();

                return RedirectToAction("Index", new {id = tipoCliente == 1 ? agendamento.PFId : agendamento.PJId, tipoCliente = tipoCliente});
            }

            ViewBag.ClientId = tipoCliente == 1 ? agendamento.PFId : agendamento.PJId;
            ViewBag.TipoCliente = tipoCliente;

            return View(agendamento);
        }

        [Authorize(Roles = "Admin, CadastroAgendamento")]
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            if (id > 0)
            {
                var model = db.Agendamento.Find(id);

                if (model != null)
                {
                    db.Agendamento.Remove(model);
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