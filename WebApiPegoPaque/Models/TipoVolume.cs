using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_tipo_volumes")]
    public class TipoVolume
    {
        [Key]
        [Column("tiv_id")]
        public int Id { get; set; }
        [Column("tiv_nome")]
        public string Nome { get; set; }
        [Column("tiv_volume")]
        public string Volume { get; set; }
        [Column("tiv_status")]
        public string Status { get; set; }
    }
}
