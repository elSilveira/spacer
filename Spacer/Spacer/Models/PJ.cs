using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spacer.Models
{
    public class PJ : Pessoa
    {
        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(200,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(200,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        //[DataType(DataType.Date)]
        [Display(Name = "Dt. Abertura")]
        public DateTime DataAbertura { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(18,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Remote("ValidarCNPJ", "PessoaJuridica", ErrorMessage = "* CNPJ inválido!")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(15,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        public virtual ICollection<Agendamento> Agendamento { get; set; }
    }
}