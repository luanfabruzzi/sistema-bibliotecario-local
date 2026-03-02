using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaApi.Models
{
    public class Agendamento
    {
        [Key]
        public int IdAgendamento { get; set; }

        [Required]
        public int IdAluno { get; set; }

        [Required]
        public int IdLivro { get; set; }

        public DateTime DataAgendamento { get; set; }

        public DateTime? DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        [Required]
        public string Status { get; set; } = "pendente"; // pendente, emprestado, devolvido, rejeitado

        // Navigation properties
        [ForeignKey("IdAluno")]
        public Aluno Aluno { get; set; } = null!;

        [ForeignKey("IdLivro")]
        public Livro Livro { get; set; } = null!;
    }
}