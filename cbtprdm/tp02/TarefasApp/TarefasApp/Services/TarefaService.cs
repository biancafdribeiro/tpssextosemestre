using System.Collections.ObjectModel;
using TarefasApp.Models;

namespace TarefasApp.Services;

public class TarefaService
{
    public ObservableCollection<TarefaItem> Tarefas { get; } = new();

    public TarefaService()
    {
        Tarefas.Add(new TarefaItem
        {
            Titulo = "Estudar .NET MAUI",
            Descricao = "Revisar navegação hierárquica e modais",
            DataCriacao = DateTime.Now.AddDays(-2),
            DataAtualizacao = DateTime.Now.AddDays(-2),
            Prioridade = Prioridade.Alta
        });

        Tarefas.Add(new TarefaItem
        {
            Titulo = "Comprar mantimentos",
            Descricao = "Leite, ovos, pão e café",
            DataCriacao = DateTime.Now.AddDays(-1),
            DataAtualizacao = DateTime.Now.AddDays(-1),
            Prioridade = Prioridade.Baixa
        });

        Tarefas.Add(new TarefaItem
        {
            Titulo = "Reunião de projeto",
            Descricao = "Alinhar entregas da sprint com a equipe",
            DataCriacao = DateTime.Now,
            DataAtualizacao = DateTime.Now,
            Prioridade = Prioridade.Media
        });
    }

    public void Adicionar(TarefaItem tarefa) => Tarefas.Insert(0, tarefa);

    public void Remover(TarefaItem tarefa)
    {
        var existente = Tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
        if (existente != null)
            Tarefas.Remove(existente);
    }

    public void Atualizar(TarefaItem tarefaAtualizada)
    {
        var existente = Tarefas.FirstOrDefault(t => t.Id == tarefaAtualizada.Id);
        if (existente is null) return;

        existente.Titulo = tarefaAtualizada.Titulo;
        existente.Descricao = tarefaAtualizada.Descricao;
        existente.DataAtualizacao = tarefaAtualizada.DataAtualizacao;
        existente.Prioridade = tarefaAtualizada.Prioridade;

        var indiceAtual = Tarefas.IndexOf(existente);
        if (indiceAtual > 0)
            Tarefas.Move(indiceAtual, 0);
    }
}