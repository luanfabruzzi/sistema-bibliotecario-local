using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    IdAluno = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.IdAluno);
                });

            migrationBuilder.CreateTable(
                name: "Diretores",
                columns: table => new
                {
                    IdDiretor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diretores", x => x.IdDiretor);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    IdLivro = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    Indicacao = table.Column<string>(type: "TEXT", nullable: false),
                    Disciplina = table.Column<string>(type: "TEXT", nullable: false),
                    Disponivel = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.IdLivro);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    IdAgendamento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdAluno = table.Column<int>(type: "INTEGER", nullable: false),
                    IdLivro = table.Column<int>(type: "INTEGER", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DataDevolucao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.IdAgendamento);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Alunos_IdAluno",
                        column: x => x.IdAluno,
                        principalTable: "Alunos",
                        principalColumn: "IdAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Livros_IdLivro",
                        column: x => x.IdLivro,
                        principalTable: "Livros",
                        principalColumn: "IdLivro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diretores",
                columns: new[] { "IdDiretor", "DataCadastro", "Email", "Nome", "Senha" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "inahdabiblioteca2025@gmail.com", "Administrador", "$2y$10$U4UGWsCNc3MN0oq.nk0hsuQib1d4Vyhx6DH4GWgETLOMjN7ffs/hW" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sandraferreirasoares@prof.educacao.sp.gov.br", "Sandra", "$2y$10$A7Of.Wf0fULxFObwc48keeFE9vVlWxCtHKECLBJuIDBVydRYog8am" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "erikasuzuki@professor.educacao.sp.gov.br", "Erika", "$2y$10$g1M7JMtJYTQDNgu59TrrkOb94fRGKe3R2hU6PFJek0BcBRKVdJ6t2" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "thiagovargas@professor.educacao.sp.gov.br", "Thiago", "$2y$10$vbZUL5RCZT/WGMBU8Tc5CuzQ1BYvM9z/LZoU9xcchmPNroVWQPtWG" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "raquelmena@professor.educacao.sp.gov.br", "Raquel", "$2y$10$HI/mX7ZKfYhaVPrknN8N8uRfAgqRQXudNPnIcoN3eFKtYYwkqTLGG" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "prisciladucci@professor.educacao.sp.gov.br", "Priscila", "$2y$10$1Pf.aVOy9dYijTH9ICXeNuyCTrEaD27Gs7oPP1khXZJFn1JQg2EcS" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Solange.oliveira11@servidor.educacao.sp.gov.br", "Solange", "$2y$10$5CIUiwxX/j2AXUo5QqwDNu9wM4PcfopHzBP4vu.Oxv27RYD3ohWU6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdAluno",
                table: "Agendamentos",
                column: "IdAluno");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_IdLivro",
                table: "Agendamentos",
                column: "IdLivro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "Diretores");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
