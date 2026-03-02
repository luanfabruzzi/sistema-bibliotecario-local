// Gerenciamento de Agendamentos - Direção - Biblioteca Online INAH DE MELLO

// Alternar menu
function alternarMenu() {
  document.querySelector('.menuV').classList.toggle('active');
}

// Carregar agendamentos
async function carregarAgendamentos() {
  try {
    const resposta = await fetch('http://localhost:5162/api/direcao/obter_agendamentos');
    const dados = await resposta.json();
    
    const tbody = document.querySelector('#listaAgendamentos tbody');
    tbody.innerHTML = '';
    
    if (dados && dados.length > 0) {
      dados.agendamentos.forEach(agendamento => {
        const linha = document.createElement('tr');
        
        // Formatar data
        const data = new Date(agendamento.data_agendamento);
        const dataFormatada = data.toLocaleDateString('pt-BR');
        
        // Traduzir status
        let statusTexto = agendamento.status;
        switch(agendamento.status) {
          case 'pendente': statusTexto = 'Pendente'; break;
          case 'emprestado': statusTexto = 'Emprestado'; break;
        }
        
        // Botões de ação conforme status
        let botoesAcao = '';
        if (agendamento.status === 'pendente') {
          botoesAcao = `
            <button class="btn btn-sm btn-success" onclick="aceitarEmprestimo(${agendamento.id_agendamento})">
              Aceitar Empréstimo
            </button>
            <button class="btn btn-sm btn-danger" onclick="rejeitarAgendamento(${agendamento.id_agendamento})">
              Rejeitar
            </button>
          `;
        } else if (agendamento.status === 'emprestado') {
          botoesAcao = `
            <button class="btn btn-sm btn-primary" onclick="confirmarDevolucao(${agendamento.id_agendamento})">
              Confirmar Devolução
            </button>
          `;
        }
        
        linha.innerHTML = `
          <td>${agendamento.nome_aluno}</td>
          <td>${agendamento.titulo_livro}</td>
          <td>${dataFormatada}</td>
          <td><span class="badge bg-${agendamento.status === 'pendente' ? 'warning' : 'info'}">${statusTexto}</span></td>
          <td>${botoesAcao}</td>
        `;
        tbody.appendChild(linha);
      });
    } else {
      document.getElementById('mensagem').textContent = 'Nenhum agendamento encontrado.';
    }
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').textContent = 'Erro ao carregar agendamentos.';
  }
}

// Aceitar empréstimo
async function aceitarEmprestimo(id) {
  if (!confirm('Aceitar o empréstimo deste agendamento?')) return;
  
  const formData = new FormData();
  formData.append('id', id);
  
  try {
    const resposta = await fetch('http://localhost:5162/api/direcao/aceitar_emprestimo', {
      method: 'POST',
      body: formData
    });
    const dados = await resposta.json();
    
    document.getElementById('mensagem').textContent = dados.message || 'Empréstimo aceito!';
    carregarAgendamentos();
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').textContent = 'Erro ao aceitar empréstimo.';
  }
}

// Rejeitar agendamento
async function rejeitarAgendamento(id) {
  if (!confirm('Rejeitar este agendamento?')) return;
  
  const formData = new FormData();
  formData.append('id', id);
  
  try {
    const resposta = await fetch('http://localhost:5162/api/direcao/rejeitar_agendamento', {
      method: 'POST',
      body: formData
    });
    const dados = await resposta.json();
    
    document.getElementById('mensagem').textContent = dados.message || 'Agendamento rejeitado!';
    carregarAgendamentos();
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').textContent = 'Erro ao rejeitar agendamento.';
  }
}

// Confirmar devolução
async function confirmarDevolucao(id) {
  if (!confirm('Confirmar a devolução deste agendamento?')) return;
  
  const formData = new FormData();
  formData.append('id', id);
  
  try {
    const resposta = await fetch('http://localhost:5162/api/direcao/confirmar_devolucao', {
      method: 'POST',
      body: formData
    });
    const dados = await resposta.json();
    
    document.getElementById('mensagem').textContent = dados.message || 'Devolução confirmada!';
    carregarAgendamentos();
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').textContent = 'Erro ao confirmar devolução.';
  }
}

// Carregar ao iniciar a página
document.addEventListener('DOMContentLoaded', carregarAgendamentos);
