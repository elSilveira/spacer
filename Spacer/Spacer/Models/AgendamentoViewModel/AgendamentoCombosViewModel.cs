using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spacer.Models.AgendamentoViewModel
{
    public class AgendamentoCombosViewModel
    {
        public IList<ViewModelBase> Periodos { get; set; }
        public IList<ViewModelBase> Espacos { get; set; }
        public IList<ViewModelBase> FormasPagamento { get; set; }

        public AgendamentoCombosViewModel()
        {
            this.Periodos = new List<ViewModelBase>();
            this.Espacos = new List<ViewModelBase>();
            this.FormasPagamento = new List<ViewModelBase>();
        }
        
        public void CarregarDadosCombosAgendamento(IList<Espaco> espacos, IList<FormaPagamento> formaPagamentos)
        {
            foreach (Periodo p in Enum.GetValues(typeof(Periodo)))
            {
                Periodos.Add(new ViewModelBase { Id = (int)p, Nome = ((Periodo)p).ToString() });
            }

            foreach (var e in espacos)
            {
                Espacos.Add(new ViewModelBase {Id = e.Id, Nome = e.Nome});
            }

            foreach (var fp in formaPagamentos)
            {
                FormasPagamento.Add(new ViewModelBase {Id = fp.Id, Nome = fp.Nome});
            }
        }
    }

    public class ViewModelBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}