using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;

namespace Spacer.Models
{
    public class PF : Pessoa
    {
        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(200,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        //[DataType(DataType.Date)]
        [Display(Name = "Dt. Nasc.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(14,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Remote("ValidarCPF", "PessoaFisica", ErrorMessage = "* CPF inválido!")]
        public string CPF { get; set; }
        
        [StringLength(20,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        public string RG { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Gênero")]
        public Genero Genero { get; set; }

        public virtual  ICollection<Agendamento> Agendamento { get; set; }
    }
}