using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    public class ReturnToken
    {
        public string Error { get; set; }
        public Usuario user { get; set; } = new Usuario();

        public string Token { get; set; }
    }
}
