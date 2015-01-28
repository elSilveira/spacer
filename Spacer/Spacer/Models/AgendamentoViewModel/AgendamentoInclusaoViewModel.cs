using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spacer.Models.AgendamentoViewModel
{
    public class AgendamentoInclusaoViewModel
    {
        public DateTime DataAgendamento { get; set; }
        public decimal Valor { get; set; }
        public int Periodo { get; set; }
        public int Espaco { get; set; }
        public int FormaPagamento { get; set; }
    }
}