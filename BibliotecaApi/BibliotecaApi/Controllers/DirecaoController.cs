using BibliotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirecaoController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public DirecaoController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return Content("<script>alert('Preencha todos os campos!'); window.location.href='http://localhost:8000/html/LoginDirecao.html';</script>", "text/html");
            }

            var diretor = await _context.Diretores.FirstOrDefaultAsync(d => d.Email == email);
            if (diretor == null || !VerifyPassword(senha, diretor.Senha))
            {
                return Content("<script>alert('Email ou senha incorretos!'); window.location.href='http://localhost:8000/html/LoginDirecao.html';</script>", "text/html");
            }

            return Content("<script>window.location.href='http://localhost:8000/html/PaginaDirecao.html';</script>", "text/html");
        }

        [HttpPost("register_book")]
        public async Task<IActionResult> RegisterBook([FromForm] string titulo, [FromForm] string autor, [FromForm] string indicacao, [FromForm] string disciplina)
        {
            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(autor) || string.IsNullOrEmpty(indicacao) || string.IsNullOrEmpty(disciplina))
            {
                return Content("<script>alert('Preencha todos os campos!'); history.back();</script>", "text/html");
            }

            var livro = new Livro { Titulo = titulo, Autor = autor, Indicacao = indicacao, Disciplina = disciplina, DataCadastro = DateTime.Now };
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return Content("<script>alert('Livro cadastrado com sucesso!'); history.back();</script>", "text/html");
        }

        [HttpGet("obter_agendamentos")]
        public async Task<IActionResult> ObterAgendamentos()
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Aluno)
                .Include(a => a.Livro)
                .ToListAsync();
            return Ok(agendamentos);
        }

        [HttpPost("aceitar_emprestimo")]
        public async Task<IActionResult> AceitarEmprestimo([FromForm] int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.Status = "emprestado";
            agendamento.DataEmprestimo = DateTime.Now;

            var livro = await _context.Livros.FindAsync(agendamento.IdLivro);
            if (livro != null)
            {
                livro.Disponivel = false;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Empréstimo aceito!" });
        }

        [HttpPost("rejeitar_agendamento")]
        public async Task<IActionResult> RejeitarAgendamento([FromForm] int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.Status = "rejeitado";
            await _context.SaveChangesAsync();
            return Ok(new { message = "Agendamento rejeitado!" });
        }

        [HttpPost("confirmar_devolucao")]
        public async Task<IActionResult> ConfirmarDevolucao([FromForm] int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            agendamento.Status = "devolvido";
            agendamento.DataDevolucao = DateTime.Now;

            var livro = await _context.Livros.FindAsync(agendamento.IdLivro);
            if (livro != null)
            {
                livro.Disponivel = true;
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Devolução confirmada!" });
        }

        [HttpGet("listar_livros")]
        public async Task<IActionResult> ListarLivros()
        {
            var livros = await _context.Livros.ToListAsync();
            return Ok(livros);
        }

        [HttpPost("change_password")]
        public async Task<IActionResult> ChangePassword([FromForm] int idDiretor, [FromForm] string novaSenha)
        {
            var diretor = await _context.Diretores.FindAsync(idDiretor);
            if (diretor == null)
            {
                return NotFound();
            }

            diretor.Senha = HashPassword(novaSenha);
            await _context.SaveChangesAsync();
            return Content("<script>alert('Senha alterada com sucesso!'); window.location.href='http://localhost:8000/html/PaginaDirecao.html';</script>", "text/html");
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}