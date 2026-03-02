using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class Livro
    {
        [Key]
        public int IdLivro { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string Autor { get; set; } = string.Empty;

        [Required]
        public string Indicacao { get; set; } = string.Empty;

        [Required]
        public string Disciplina { get; set; } = string.Empty;

        public bool Disponivel { get; set; } = true;

        public DateTime DataCadastro { get; set; }

        // Navigation property
        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }
}