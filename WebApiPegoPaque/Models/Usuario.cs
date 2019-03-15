using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPegoPaque.Models
{
    [Table("tb_usuarios")]
    public class Usuario
    {
        [Column("usu_id")]
        public int Id { get; set; }
        [Column("usu_nome")]
        [Required(ErrorMessage ="Informe o {0}")]
        public string Nome { get; set; }
        [Column("usu_senha")]
        [Required(ErrorMessage = "Informe a {0}")]
        public string Senha { get; set; }
        [Column("usu_email")]
        [Required(ErrorMessage = "Informe o {0}")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }
        [Column("usu_dt_cadastro")]
        public DateTime DtCadastro { get; set; }
        [Column("usu_status")]
        public string Status { get; set; }
    }
}
