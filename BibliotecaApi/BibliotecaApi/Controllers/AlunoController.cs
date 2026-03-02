using BibliotecaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BibliotecaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AlunoController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] string nome, [FromForm] string email, [FromForm] string senha)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return Content("<script>alert('Preencha todos os campos!'); window.location.href='http://localhost:8000/html/CadastroAluno.html';</script>", "text/html");
            }

            // Check if already exists
            var existing = await _context.Alunos.AnyAsync(a => a.Email == email || a.Nome == nome);
            if (existing)
            {
                return Content("<script>alert('Nome ou email já cadastrado!'); window.location.href='http://localhost:8000/html/CadastroAluno.html';</script>", "text/html");
            }

            var hashedPassword = HashPassword(senha);
            var aluno = new Aluno { Nome = nome, Email = email, Senha = hashedPassword, DataCadastro = DateTime.Now };
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Content("<script>alert('Cadastro realizado com sucesso!'); window.location.href='http://localhost:8000/index.html';</script>", "text/html");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                return Content("<script>alert('Preencha todos os campos!'); window.location.href='http://localhost:8000/index.html';</script>", "text/html");
            }

            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Email == email);
            if (aluno == null || !VerifyPassword(senha, aluno.Senha))
            {
                return Content("<script>alert('Email ou senha incorretos!'); window.location.href='http://localhost:8000/index.html';</script>", "text/html");
            }

            // For simplicity, redirect to page
            return Content("<script>window.location.href='http://localhost:8000/html/PaginaAluno.html';</script>", "text/html");
        }

        [HttpGet("buscar_livros")]
        public async Task<IActionResult> BuscarLivros([FromQuery] string? titulo, [FromQuery] string? autor)
        {
            var query = _context.Livros.AsQueryable();
            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(l => l.Titulo.Contains(titulo));
            if (!string.IsNullOrEmpty(autor))
                query = query.Where(l => l.Autor.Contains(autor));

            var livros = await query.ToListAsync();
            return Ok(livros);
        }

        [HttpPost("agendar_livro")]
        public async Task<IActionResult> AgendarLivro([FromForm] int idLivro, [FromForm] int idAluno)
        {
            var livro = await _context.Livros.FindAsync(idLivro);
            if (livro == null || !livro.Disponivel)
            {
                return BadRequest(new { erro = "Livro não disponível!" });
            }

            var agendamento = new Agendamento { IdAluno = idAluno, IdLivro = idLivro, DataAgendamento = DateTime.Now };
            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Agendamento realizado!" });
        }

        [HttpGet("meus_agendamentos")]
        public async Task<IActionResult> MeusAgendamentos([FromQuery] int idAluno)
        {
            var agendamentos = await _context.Agendamentos
                .Include(a => a.Livro)
                .Where(a => a.IdAluno == idAluno)
                .ToListAsync();
            return Ok(agendamentos);
        }

        [HttpPost("alterar_senha")]
        public async Task<IActionResult> AlterarSenha([FromForm] int idAluno, [FromForm] string novaSenha)
        {
            var aluno = await _context.Alunos.FindAsync(idAluno);
            if (aluno == null)
            {
                return NotFound();
            }

            aluno.Senha = HashPassword(novaSenha);
            await _context.SaveChangesAsync();
            return Content("<script>alert('Senha alterada!'); window.location.href='http://localhost:8000/index.html';</script>", "text/html");
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