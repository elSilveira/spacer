using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Spacer.Methods.ClaimsMethods;
using Spacer.Models;
using Spacer.Models.AgendamentoViewModel;

namespace Spacer.Controllers.Api
{
    [Authorize(Roles = "CadastroAgendamentos")]
    public class AgendamentoController : ApiController
    {
        private readonly ContextoDB _db = new ContextoDB();

        [Route("api/agendamento/todos")]
        public IHttpActionResult GetTodos()
        {
            var user = new ClaimsManager().GetClaimsIdentity(User.Identity);

            var model = new AgendamentoConsultaViewModel().CarregarTodosAgendamentos(user.Id, user.TipoCliente, _db);

            return Ok(model);
        }

        [Route("api/agendamento/porid")]
        public IHttpActionResult GetPorId(int id)
        {
            var user = new ClaimsManager().GetClaimsIdentity(User.Identity);

            var model = new AgendamentoConsultaViewModel();
            model.CarregarAgendamento(id, _db);

            if (model.Id > 0)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Agendamento não encontrado!");
            }
        }

        [Route("api/agendamento/novo")]
        public IHttpActionResult GetNovo()
        {
            var agendamentoViewModel = new AgendamentoCombosViewModel();

            var espacos = _db.Espaco.ToList();
            var formas = _db.FormaPagamento.ToList();

            agendamentoViewModel.CarregarDadosCombosAgendamento(espacos, formas);

            return Ok(agendamentoViewModel);
        }

        [HttpPost]
        [Route("api/agendamento/salvar")]
        public IHttpActionResult Post([FromBody]AgendamentoInclusaoViewModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = new ClaimsManager().GetClaimsIdentity(User.Identity);

                    var agendamento = new Agendamento
                    {
                        DataAgendamento = model.DataAgendamento,
                        ValorAgendamento = model.Valor,
                        EspacoId = model.Espaco,
                        FormaPagamentoId = model.FormaPagamento,
                        PFId = user.TipoCliente == 1 ? (int?)user.Id : null,
                        PJId = user.TipoCliente == 2 ? (int?)user.Id : null,
                        Periodo = (Periodo)model.Periodo
                    };

                    _db.Agendamento.Add(agendamento);
                    _db.SaveChanges();

                    return Ok(agendamento);
                }
                else
                {
                    return BadRequest("Todos os campos são obrigatórios!");
                }
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException != null
                    ? ex.InnerException.InnerException != null
                        ? ex.InnerException.InnerException.Message
                        : ex.InnerException.Message
                    : ex.Message;

                return BadRequest(msg);
            }
        }
    }
}
