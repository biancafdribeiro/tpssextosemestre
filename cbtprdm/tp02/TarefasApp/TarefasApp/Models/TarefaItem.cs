using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TarefasApp.Models;

public enum Prioridade
{
    Baixa,
    Media,
    Alta
}

public class TarefaItem : INotifyPropertyChanged
{
    private string _titulo = string.Empty;
    private string _descricao = string.Empty;
    private DateTime _dataCriacao = DateTime.Now;
    private DateTime _dataAtualizacao = DateTime.Now;
    private Prioridade _prioridade = Prioridade.Media;
    private bool _concluida;

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Titulo
    {
        get => _titulo;
        set { _titulo = value; OnPropertyChanged(); }
    }

    public string Descricao
    {
        get => _descricao;
        set { _descricao = value; OnPropertyChanged(); }
    }

    public DateTime DataCriacao
    {
        get => _dataCriacao;
        set { _dataCriacao = value; OnPropertyChanged(); }
    }

    public DateTime DataAtualizacao
    {
        get => _dataAtualizacao;
        set { _dataAtualizacao = value; OnPropertyChanged(); }
    }

    public Prioridade Prioridade
    {
        get => _prioridade;
        set { _prioridade = value; OnPropertyChanged(); }
    }

    public bool Concluida
    {
        get => _concluida;
        set { _concluida = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? nome = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    public TarefaItem Clonar()
    {
        return new TarefaItem
        {
            Id = Id,
            Titulo = Titulo,
            Descricao = Descricao,
            DataCriacao = DataCriacao,
            DataAtualizacao = DataAtualizacao,
            Prioridade = Prioridade,
            Concluida = Concluida
        };
    }
}