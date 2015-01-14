using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spacer.Models.UsuariosViewModel
{
    public class AlteracaoSenhaViewModel
    {
        public int Id { get; set; }

        public string Login { get; set; }
        [Display(Name = "Senha Atual")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }

        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "* A nova senha e a confirmação da nova senha não são iguais!")]
        public string ConfirmacaoNovaSenha { get; set; }
    }
}