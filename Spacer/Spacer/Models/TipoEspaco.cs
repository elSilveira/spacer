using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Spacer.Models
{
    public class TipoEspaco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Obrigatório!")]
        [StringLength(30, ErrorMessage = "* Máximo de {1} caracteres!")]
        public string Nome { get; set; }
    }
}