using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPegoPaque.Models
{
    [Table("tb_estabelecimentos")]
    public class Estabelecimento
    {
        [key]
        [Column("est_id")]
        public int Id { get; set; }
        [Column("est_nome")]
        public string Nome { get; set; }
        [Column("est_endereco")]
        public string Endereco { get; set; }
        [Column("est_numero")]
        public string Numero { get; set; }
        [Column("est_bairro")]
        public string Bairro { get; set; }
        [Column("est_cidade")]
        public string Cidade { get; set; }
        [Column("est_estado")]
        public string Estado { get; set; }
        [Column("est_pais")]
        public string Pais { get; set; }
        [Column("est_tel_contato")]
        public string TelContato { get; set; }
        [Column("est_dt_cadastro")]
        public DateTime DtCadastro { get; set; }
        [Column("est_status")]
        public string Status { get; set; }
    }
}
