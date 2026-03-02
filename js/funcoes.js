// Funções JavaScript - Biblioteca Online INAH DE MELLO

// Alternar menu vertical
function alternarMenu() {
  document.querySelector('.menuV').classList.toggle('active');
}

// Mostrar modal de cadastro de livro
function mostrarCadastro() {
  document.getElementById('telaCadastro').style.display = 'block';
}

// Fechar modal de cadastro
function fecharCadastro() {
  document.getElementById('telaCadastro').style.display = 'none';
}

// Mostrar modal de alterador de senha
function mostrarAlterador() {
  document.getElementById('telaAlterador').style.display = 'block';
}

// Fechar modal de alterador
function fecharAlterador() {
  document.getElementById('telaAlterador').style.display = 'none';
}

// Buscar livros com filtros (PaginaAluno.html)
if (document.getElementById('formularioBusca')) {
  document.getElementById('formularioBusca').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    const autor = document.getElementById('autor').value;
    const titulo = document.getElementById('titulo').value;
    const indicacao = document.getElementById('indicacao').value;
    const disciplina = document.getElementById('disciplina').value;
    
    const params = new URLSearchParams();
    if (autor) params.append('autor', autor);
    if (titulo) params.append('titulo', titulo);
    if (indicacao) params.append('indicacao', indicacao);
    if (disciplina) params.append('disciplina', disciplina);
    
    try {
      const resposta = await fetch(`http://localhost:5162/api/aluno/buscar_livros?${params.toString()}`);
      const dados = await resposta.json();
      
      const resultadosDiv = document.getElementById('resultadosBusca');
      resultadosDiv.innerHTML = '';
      
      if (dados.livros && dados.livros.length > 0) {
        const tabela = document.createElement('table');
        tabela.className = 'table table-dark table-striped mt-3';
        tabela.innerHTML = `
          <thead>
            <tr>
              <th>Título</th>
              <th>Autor</th>
              <th>Indicação</th>
              <th>Disciplina</th>
              <th>Ação</th>
            </tr>
          </thead>
          <tbody id="corpoTabela"></tbody>
        `;
        resultadosDiv.appendChild(tabela);
        
        const corpo = document.getElementById('corpoTabela');
        dados.forEach(livro => {
          const linha = document.createElement('tr');
          linha.innerHTML = `
            <td>${livro.titulo}</td>
            <td>${livro.autor}</td>
            <td>${livro.indicacao}</td>
            <td>${livro.disciplina}</td>
            <td>
              <button class="btn btn-sm btn-success" onclick="agendarLivro(${livro.id_livro})">
                Agendar
              </button>
            </td>
          `;
          corpo.appendChild(linha);
        });
      } else {
        resultadosDiv.innerHTML = '<p class="text-center text-white mt-3">Nenhum livro encontrado.</p>';
      }
    } catch (erro) {
      console.error('Erro:', erro);
      document.getElementById('resultadosBusca').innerHTML = 
        '<p class="text-center text-danger mt-3">Erro ao buscar livros.</p>';
    }
  });
}

// Agendar livro
async function agendarLivro(idLivro) {
  if (!confirm('Deseja agendar este livro?')) return;
  
  const formData = new FormData();
  formData.append('idLivro', idLivro);
  formData.append('idAluno', 1); // Hardcoded for demo
  
  try {
    const resposta = await fetch('http://localhost:5162/api/aluno/agendar_livro', {
      method: 'POST',
      body: formData
    });
    const dados = await resposta.json();
    
    if (resposta.ok) {
      alert(dados.message);
      document.getElementById('formularioBusca').dispatchEvent(new Event('submit'));
    } else {
      alert(dados.erro || 'Erro ao agendar livro.');
    }
  } catch (erro) {
    console.error('Erro:', erro);
    alert('Erro ao agendar livro.');
  }
}
