// Agendamentos do Aluno - Biblioteca Online INAH DE MELLO

// Alternar menu
function alternarMenu() {
  document.querySelector('.menuV').classList.toggle('active');
}

// Carregar agendamentos do aluno
async function carregarAgendamentos() {
  try {
    const resposta = await fetch('http://localhost:5162/api/aluno/meus_agendamentos?idAluno=1');
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
          case 'devolvido': statusTexto = 'Devolvido'; break;
          case 'rejeitado': statusTexto = 'Rejeitado'; break;
        }
        
        linha.innerHTML = `
          <td>${agendamento.titulo}</td>
          <td>${agendamento.autor}</td>
          <td>${dataFormatada}</td>
          <td><span class="badge bg-${getBadgeColor(agendamento.status)}">${statusTexto}</span></td>
        `;
        tbody.appendChild(linha);
      });
    } else {
      document.getElementById('mensagem').textContent = 'Você não possui agendamentos.';
    }
  } catch (erro) {
    console.error('Erro:', erro);
    document.getElementById('mensagem').textContent = 'Erro ao carregar agendamentos.';
  }
}

// Obter cor do badge de status
function getBadgeColor(status) {
  switch(status) {
    case 'pendente': return 'warning';
    case 'emprestado': return 'info';
    case 'devolvido': return 'success';
    case 'rejeitado': return 'danger';
    default: return 'secondary';
  }
}

// Carregar ao iniciar a página
document.addEventListener('DOMContentLoaded', carregarAgendamentos);
