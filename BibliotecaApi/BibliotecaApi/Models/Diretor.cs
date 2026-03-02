using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class Diretor
    {
        [Key]
        public int IdDiretor { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }
    }
}