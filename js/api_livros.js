// API Google Books - Biblioteca Online INAH DE MELLO

// Alternar menu
function alternarMenu() {
  document.querySelector('.menuV').classList.toggle('active');
}

// Buscar livros na API do Google Books
async function buscarLivrosGoogle() {
  const consulta = document.getElementById('campoBusca').value.trim();
  const containerLivros = document.getElementById('containerLivros');
  containerLivros.innerHTML = '<p class="text-primary text-center">Carregando...</p>';

  if (!consulta) {
    containerLivros.innerHTML = '<p class="text-danger text-center">Digite um termo para buscar.</p>';
    return;
  }

  try {
    const resposta = await fetch(`https://www.googleapis.com/books/v1/volumes?q=${encodeURIComponent(consulta)}&langRestrict=pt`);
    const dados = await resposta.json();

    containerLivros.innerHTML = '';

    if (!dados.items) {
      containerLivros.innerHTML = '<p class="text-warning text-center">Nenhum livro encontrado.</p>';
      return;
    }

    dados.items.slice(0, 9).forEach(livro => {
      const infoVolume = livro.volumeInfo;

      const col = document.createElement('div');
      col.className = 'col-12 col-sm-6 col-md-4 d-flex';

      const miniatura = infoVolume.imageLinks?.thumbnail || '../imagens/sem-imagem.png';
      const titulo = infoVolume.title || 'Sem título';
      const autores = infoVolume.authors ? infoVolume.authors.join(', ') : 'Autor desconhecido';
      const linkPrevia = infoVolume.previewLink || '#';

      col.innerHTML = `
        <div class="card p-2 d-flex flex-column" style="background-color: rgba(0, 0, 0, 0.7); color: white;">
          <img src="${miniatura}" class="card-img-top mb-2" alt="Capa do livro" style="max-height: 200px; object-fit: contain;">
          <div class="card-body d-flex flex-column">
            <h5 class="card-title">${titulo}</h5>
            <p class="card-text">${autores}</p>
            <a href="${linkPrevia}" target="_blank" class="mt-auto btn btn-sm btn-primary">Ler online</a>
          </div>
        </div>
      `;

      containerLivros.appendChild(col);
    });
  } catch (erro) {
    containerLivros.innerHTML = '<p class="text-danger text-center">Erro ao buscar livros. Tente novamente.</p>';
    console.error('Erro:', erro);
  }
}

// Permitir busca com Enter
document.getElementById('campoBusca').addEventListener('keypress', function(e) {
  if (e.key === 'Enter') {
    buscarLivrosGoogle();
  }
});
