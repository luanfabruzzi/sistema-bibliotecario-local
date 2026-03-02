// Lista de Livros - Direção - Biblioteca Online INAH DE MELLO

// Alternar menu
function alternarMenu() {
  document.querySelector('.menuV').classList.toggle('active');
}

// Carregar todos os livros
async function carregarLivros() {
  try {
    const resposta = await fetch('http://localhost:5162/api/direcao/listar_livros');
    const dados = await resposta.json();
    
    const tbody = document.querySelector('#listaLivros tbody');
    tbody.innerHTML = '';
    
    if (resposta.ok) {
      document.getElementById('totalLivros').textContent = dados.length;
      
      if (dados && dados.length > 0) {
        dados.forEach(livro => {
          const linha = document.createElement('tr');
          
          const data = new Date(livro.data_cadastro);
          const dataFormatada = data.toLocaleDateString('pt-BR');
          
          const statusTexto = livro.disponivel == 1 ? 'Disponível' : 'Indisponível';
          const statusClass = livro.disponivel == 1 ? 'success' : 'secondary';
          
          linha.innerHTML = `
            <td>${livro.titulo}</td>
            <td>${livro.autor}</td>
            <td>${livro.indicacao}</td>
            <td>${livro.disciplina}</td>
            <td><span class="badge bg-${statusClass}">${statusTexto}</span></td>
            <td>${dataFormatada}</td>
          `;
          tbody.appendChild(linha);
        });
      } else {
        document.getElementById('mensagem').innerHTML = 
          '<p class="text-warning">Nenhum livro cadastrado ainda. Use a opção "Cadastro de Livros" no menu principal para adicionar livros.</p>';
      }
    } else {
      document.getElementById('mensagem').innerHTML = 
        '<p class="text-danger">Erro ao carregar livros.</p>';
    }
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').innerHTML = 
      '<p class="text-danger">Erro ao carregar livros. Por favor, tente novamente.</p>';
  }
}

// Carregar ao iniciar a página
document.addEventListener('DOMContentLoaded', carregarLivros);
