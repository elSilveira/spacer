using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spacer.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Data")]
        public DateTime DataAgendamento { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Valor")]
        public decimal ValorAgendamento { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Valor Pago")]
        public decimal ValorPago { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Período")]
        public Periodo Periodo { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Espaço")]
        public int EspacoId { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Forma Pgto")]
        public int FormaPagamentoId { get; set; }

        [Display(Name = "Cliente")]
        public int? PFId { get; set; }

        [Display(Name = "Cliente")]
        public int? PJId { get; set; }

        public virtual Espaco Espaco { get; set; }

        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual PF PF { get; set; }

        public virtual PJ PJ { get; set; }
    }
}