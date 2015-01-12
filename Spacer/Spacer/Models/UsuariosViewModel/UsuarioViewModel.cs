using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spacer.Models.UsuariosViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "* Obrigatório!")]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "* Máximo de {1} e mínimo de {2} caracteres!", MinimumLength = 4)]
        public string Senha { get; set; }
        
        [Display(Name = "Manter-me conectado")]
        public bool RememberMe { get; set; }
    }

    public class SenhaUsuarioViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "* Máximo de {1} e mínimo de {2} caracteres!", MinimumLength = 6)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da senha")]
        [StringLength(20, ErrorMessage = "* Máximo de {1} e mínimo de {2} caracteres!", MinimumLength = 6)]
        [Compare("Senha", ErrorMessage = "* A senha e a confirmação da senha não são iguais.")]
        public string ConfirmacaoSenha { get; set; }
    }
}