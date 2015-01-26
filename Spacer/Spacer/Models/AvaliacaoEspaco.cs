using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spacer.Models
{
    public class AvaliacaoEspaco
    {
        [Key]
        public int Id { get; set; }
        
        public decimal Estrelas { get; set; }
        
        public int? PFId { get; set; }

        public int? PJId { get; set; }

        public int EspacoId { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}