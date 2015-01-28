using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spacer.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(200, ErrorMessage = "* O campo {0} aceita no máximo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(20, 
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!", 
            MinimumLength = 5)]
        [Remote("ValidarUsuarioUnico", "Usuario", ErrorMessage = "* Usuário já existente!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "* A senha e a confirmação da senha não são iguais!")]
        public string ConfirmacaoSenha { get; set; }

        public int TipoCliente { get; set; }

        public virtual ICollection<Permissao> Permissoes { get; set; }
    }
}