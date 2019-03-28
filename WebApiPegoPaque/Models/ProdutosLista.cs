using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_produtos_lista")]
    public class ProdutosLista
    {
        [key]
        [Column("prl_id")]
        public int Id { get; set; }
        [Column("prl_lim_id")]
        public int LimId { get; set; }
        [Column("prl_obs")]
        public string Obs { get; set; }
        [Column("prl_qtd")]
        public double Qtd { get; set; }
        [Column("prl_volume")]
        public string Volume { get; set; }
        [Column("prl_tiv_id")]
        public int TivId { get; set; }
        [Column("prl_pro_nome")]
        public string ProNome { get; set; }
        [Column("prl_pro_id")]
        public int ProId { get; set; }
        [Column("prl_mar_id")]
        public int MarId { get; set; }
        [Column("prl_mar_nome")]
        public string MarNome { get; set; }
    }
}
