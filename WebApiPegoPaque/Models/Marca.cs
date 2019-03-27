using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_marcas")]
    public class Marca
    {
        [Key]
        [Column("mar_id")]
        public int Id { get; set; }
        [Column("mar_nome")]
        public string Nome { get; set; }
        [Column("mar_data_cad")]
        public DateTime DataCadastro { get; set; }
        [Column("mar_status")]
        public string Status { get; set; }
    }
}
