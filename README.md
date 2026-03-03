# BibliotecaApi Backend

Este diretório contém a implementação da API de backend do sistema de biblioteca local. Trata-se de um projeto ASP.NET Core (versão .NET 10.0) que expõe endpoints REST para autenticação e gerenciamento de recursos como alunos, diretores, livros e agendamentos.

## Visão geral do projeto

- **Tipo de projeto**: ASP.NET Core Web API
- **Persistência**: Entity Framework Core com SQLite (padrão) ou outro provedor configurado em `appsettings.json`.
- **Estrutura**:
  - `Controllers/` – classes que implementam os controllers MVC (parte "C").
  - `Models/` – entidades de domínio e contexto de banco de dados EF (parte "M").
  - `Migrations/` – arquivos gerados pelo EF Core para controle de versão do esquema.
  - `Program.cs` – ponto de entrada, configuração de serviços e middleware.
  - `appsettings.json`/`appsettings.Development.json` – configurações de ambiente, incluindo string de conexão.

A aplicação usa o padrão **MVC** (Model‑View‑Controller) de forma mínima: como API REST, não há views físicas, mas o pipeline segue a divisão de responsabilidades.

## Funcionalidades principais

1. **Autenticação**
   - Login de alunos e diretores via credenciais (usuário e senha).
   - Uso de tokens JWT para proteger rotas (configuração de middleware em `Program.cs`).

2. **Gerenciamento de Alunos**
   - CRUD completo (criar, ler, atualizar, excluir) para o recurso `Aluno`.
   - Validação de dados básicos e atributos não anuláveis.

3. **Gerenciamento de Diretores**
   - Operações semelhantes às de alunos para o recurso `Diretor`.
   - Controlador separado (`DirecaoController`) com rotas dedicadas.

4. **Livros**
   - API para listar livros, consultar disponibilidade, cadastrar novos exemplares e atualizar detalhes.
   - Possibilidade de integrar pesquisa/filtragem básica.

5. **Agendamentos**
   - Criação de agendamentos de reserva de livros por alunos.
   - Consulta de agendamentos existentes (por aluno, por livro, por período).
   - Cancelamento ou conclusão de agendamentos.

6. **Banco de dados**
   - Contexto `BibliotecaContext` deriva de `DbContext` e configura os `DbSet<T>` para cada entidade.
   - Migrations mantêm o histórico do esquema; use `dotnet ef migrations add <nome>` e `dotnet ef database update` para aplicar alterações.

7. **Configurações e ambiente**
   - Strings de conexão e outras opções são armazenadas em `appsettings.*.json`; `appsettings.Development.json` pode conter valores de desenvolvimento.
   - Variáveis de ambiente podem substituir as configurações, útil para CI/CD ou produção.

## Como executar

1. Navegue até a pasta do projeto (contendo o `.csproj`).
2. Restaure e compile (a partir da pasta que contém o `.csproj`):
   ```powershell
   cd BibliotecaApi\BibliotecaApi      # apenas se estiver na raiz do repositório
   dotnet restore
   dotnet build
   ```
3. Execute a API:
   ```powershell
   dotnet run
   ```
   Se preferir iniciar o projeto a partir da raiz do repositório, indique o caminho ao arquivo de projeto:
   ```powershell
   dotnet run --project "BibliotecaApi/BibliotecaApi/BibliotecaApi.csproj"
   ```
   por padrão, a aplicação ficará disponível em `http://localhost:5162` e `https://localhost:7245`.
4. Use ferramentas como `curl`, Postman ou o frontend estático incluído no repositório para testar os endpoints.


## MVC no contexto da API

- **Model**: classes em `Models/` como `Aluno`, `Diretor`, `Livro`, `Agendamento` representam os dados. O `BibliotecaContext` implementa o `DbContext` e configura as tabelas.
- **View**: não há arquivos `.cshtml`; as respostas são JSON serializadas automaticamente pelos controllers.
- **Controller**: cada classe em `Controllers/` herda de `ControllerBase` e define rotas usando atributos (`[HttpGet]`, `[HttpPost]` etc.).

Os controllers interagem com o `DbContext` (injetado via DI) para executar operações de CRUD, e devolvem objetos ou códigos de status HTTP apropriados.

## Evolução e manutenção

- Adicione novas entidades em `Models/` e crie migrations ao alterar o modelo.
- Escreva novos controllers para novos recursos ou funcionalidades.
- Configure políticas de autorização em `Program.cs` conforme necessário.
- Para testes futuros, considere criar um projeto xUnit/NUnit/MSTest e injetar `DbContext` com um banco em memória.

---

Este README procura documentar a API backend até o núcleo MVC; para detalhes de implementação consulte os arquivos fonte nas pastas mencionadas e os comentários dentro do código.