using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_listas_modelos")]
    public class ListaModelo
    {
        [Column("lim_id")]
        [key]
        public int Id { get; set; }
        [Column("lim_usu_id")]
        public int UsuId { get; set; }
        [Column("lim_nome")]
        public string Nome { get; set; }
        [Column("lim_dt_cadastro")]
        public DateTime DtCadastro { get; set; }
        [Column("lim_status")]
        public string Status { get; set; }
    }
}
