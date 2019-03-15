using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPegoPaque.Models
{
    [Table("cliente")]
    public class Cliente
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [Column("data_cadastro")]
        public DateTime DataCadastro { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [Column("cpf_cnpj")]
        public string CpfCnpj { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio!")]
        public string Tipo { get; set; }
        public string Telefone { get; set; }
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }

    }
}
