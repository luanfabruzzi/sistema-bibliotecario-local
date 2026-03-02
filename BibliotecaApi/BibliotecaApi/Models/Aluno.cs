using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class Aluno
    {
        [Key]
        public int IdAluno { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Senha { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        // Navigation property
        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}