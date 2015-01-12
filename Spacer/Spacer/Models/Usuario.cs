using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Login { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(50,
            ErrorMessage = "* O campo {0} aceita no máximo {1} e no mínimo {2} caracteres!",
            MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public virtual ICollection<Permissao> Permissoes { get; set; }
    }
}