using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spacer.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(300,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(50,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(9,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(15,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Fone Principal")]
        public string FonePrincipal { get; set; }

        [StringLength(15,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [Display(Name = "Fone Secundário")]
        public string FoneSecundario { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(100,
            ErrorMessage = "* Máximo de {1} caracteres!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "* Email inválido!")]
        [RegularExpression(@"^([\w\-]+\.)*[\w\- ]+@([\w\- ]+\.)+([\w\-]{2,3})$", ErrorMessage = "* Email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(20,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [Remote("ValidarUsuarioUnico", "PessoaFisica", ErrorMessage = "* Usuário já existente!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}