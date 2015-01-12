using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spacer.Models
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Chave { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
