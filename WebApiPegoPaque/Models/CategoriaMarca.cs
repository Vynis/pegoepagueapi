using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_categoria_marcas")]
    public class CategoriaMarca
    {
        [Key]
        [Column("cam_id")]
        public int Id { get; set; }
        [Column("cam_pro_id")]
        public int? ProId { get; set; }
        [Column("cam_mar_id")]
        public int? MarId { get; set; }
    }
}
