using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_produtos")]
    public class Produto
    {
        [Column("pro_id")]
        [Key]
        public int Id { get; set; }
        [Column("pro_nome")]
        public string Nome { get; set; }
        [Column("pro_dt_cadastro")]
        public DateTime DataCadastro { get; set; }
        [Column("pro_status")]
        public string Status { get; set; }
    }
}
