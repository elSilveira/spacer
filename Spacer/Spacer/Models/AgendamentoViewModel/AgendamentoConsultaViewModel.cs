using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spacer.Models.AgendamentoViewModel
{
    public class AgendamentoConsultaViewModel
    {
        public int Id { get; set; }
        public string DataAgendamento { get; set; }
        public string ValorAgendamento { get; set; }
        public string ValorPago { get; set; }
        public string Periodo { get; set; }
        public string Espaco { get; set; }
        public string FormaPagamento { get; set; }

        public IList<AgendamentoConsultaViewModel> CarregarTodosAgendamentos(int clienteId, int tipoCliente, ContextoDB db)
        {
            var lista = new List<AgendamentoConsultaViewModel>();

            if (tipoCliente == 1)
            {
                lista = db.Agendamento.Where(w => w.PFId == clienteId).ToList().Select(s => new AgendamentoConsultaViewModel
                {
                    Id = s.Id,
                    DataAgendamento = s.DataAgendamento.ToString("dd/MM/yyyy"),
                    ValorAgendamento = s.ValorAgendamento.ToString("N2"),
                    ValorPago = s.ValorPago.ToString("N2"),
                    Periodo = ((Periodo)s.Periodo).ToString(),
                    Espaco = s.Espaco.Nome,
                    FormaPagamento = s.FormaPagamento.Nome
                }).ToList();
            }
            else
            {
                lista = db.Agendamento.Where(w => w.PJId == clienteId).ToList().Select(s => new AgendamentoConsultaViewModel
                {
                    Id = s.Id,
                    DataAgendamento = s.DataAgendamento.ToString("dd/MM/yyyy"),
                    ValorAgendamento = s.ValorAgendamento.ToString("N2"),
                    ValorPago = s.ValorPago.ToString("N2"),
                    Periodo = ((Periodo)s.Periodo).ToString(),
                    Espaco = s.Espaco.Nome,
                    FormaPagamento = s.FormaPagamento.Nome
                }).ToList();
            }

            return lista;
        }

        public void CarregarAgendamento(int id, ContextoDB db)
        {
            var agendamento = db.Agendamento.Find(id);

            if (agendamento != null)
            {
                this.Id = agendamento.Id;
                this.DataAgendamento = agendamento.DataAgendamento.ToString("dd/MM/yyyy");
                this.ValorAgendamento = agendamento.ValorAgendamento.ToString("N2");
                this.ValorPago = agendamento.ValorPago.ToString("N2");
                this.Periodo = ((Periodo)agendamento.Periodo).ToString();
                this.Espaco = agendamento.Espaco.Nome;
                this.FormaPagamento = agendamento.FormaPagamento.Nome;
            }
        }
    }
}