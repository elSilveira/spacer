using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Email { get; set; }
    }
}