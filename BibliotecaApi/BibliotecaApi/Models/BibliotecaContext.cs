using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Diretor> Diretores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Aluno)
                .WithMany(al => al.Agendamentos)
                .HasForeignKey(a => a.IdAluno)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Livro)
                .WithMany(l => l.Agendamentos)
                .HasForeignKey(a => a.IdLivro)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for Diretores
            modelBuilder.Entity<Diretor>().HasData(
                new Diretor { IdDiretor = 1, Nome = "Administrador", Email = "inahdabiblioteca2025@gmail.com", Senha = "$2y$10$U4UGWsCNc3MN0oq.nk0hsuQib1d4Vyhx6DH4GWgETLOMjN7ffs/hW" },
                new Diretor { IdDiretor = 2, Nome = "Sandra", Email = "sandraferreirasoares@prof.educacao.sp.gov.br", Senha = "$2y$10$A7Of.Wf0fULxFObwc48keeFE9vVlWxCtHKECLBJuIDBVydRYog8am" },
                new Diretor { IdDiretor = 3, Nome = "Erika", Email = "erikasuzuki@professor.educacao.sp.gov.br", Senha = "$2y$10$g1M7JMtJYTQDNgu59TrrkOb94fRGKe3R2hU6PFJek0BcBRKVdJ6t2" },
                new Diretor { IdDiretor = 4, Nome = "Thiago", Email = "thiagovargas@professor.educacao.sp.gov.br", Senha = "$2y$10$vbZUL5RCZT/WGMBU8Tc5CuzQ1BYvM9z/LZoU9xcchmPNroVWQPtWG" },
                new Diretor { IdDiretor = 5, Nome = "Raquel", Email = "raquelmena@professor.educacao.sp.gov.br", Senha = "$2y$10$HI/mX7ZKfYhaVPrknN8N8uRfAgqRQXudNPnIcoN3eFKtYYwkqTLGG" },
                new Diretor { IdDiretor = 6, Nome = "Priscila", Email = "prisciladucci@professor.educacao.sp.gov.br", Senha = "$2y$10$1Pf.aVOy9dYijTH9ICXeNuyCTrEaD27Gs7oPP1khXZJFn1JQg2EcS" },
                new Diretor { IdDiretor = 7, Nome = "Solange", Email = "Solange.oliveira11@servidor.educacao.sp.gov.br", Senha = "$2y$10$5CIUiwxX/j2AXUo5QqwDNu9wM4PcfopHzBP4vu.Oxv27RYD3ohWU6" }
            );
        }
    }
}