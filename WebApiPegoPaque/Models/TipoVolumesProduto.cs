using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_tipo_volumes_produto")]
    public class TipoVolumesProduto
    {
        [key]
        [Column("tvp_id")]
        public int Id { get; set; }
        [Column("tvp_tiv_id")]
        public int? TivId { get; set; }
        [Column("tvp_pro_id")]
        public int ProId { get; set; }

    }
}
