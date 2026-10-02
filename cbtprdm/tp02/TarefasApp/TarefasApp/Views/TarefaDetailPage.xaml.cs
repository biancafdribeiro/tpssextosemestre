using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Views;

/// <summary>
/// Implementa IQueryAttributable para receber a tarefa selecionada como parâmetro
/// de navegação (passagem de dados entre páginas via Shell).
/// </summary>
[QueryProperty(nameof(Tarefa), "Tarefa")]
public partial class TarefaDetailPage : ContentPage, IQueryAttributable
{
    private readonly TarefaService _tarefaService;

    private TarefaItem? _tarefa;
    public TarefaItem? Tarefa
    {
        get => _tarefa;
        set
        {
            _tarefa = value;
            BindingContext = _tarefa;
        }
    }

    public TarefaDetailPage(TarefaService tarefaService)
    {
        InitializeComponent();
        _tarefaService = tarefaService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Tarefa", out var valor) && valor is TarefaItem tarefa)
        {
            Tarefa = tarefa;
        }
    }

    /// <summary>
    /// Abre o modal de edição, passando a tarefa atual para preencher os campos.
    /// </summary>
    private async void OnEditarClicked(object sender, EventArgs e)
    {
        if (Tarefa is null) return;

        var paginaEditar = new EditarTarefaPage(_tarefaService, Tarefa);
        await Navigation.PushModalAsync(new NavigationPage(paginaEditar));
    }

    /// <summary>
    /// Exibe um diálogo de confirmação antes de excluir a tarefa.
    /// </summary>
    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        if (Tarefa is null) return;

        bool confirmar = await DisplayAlert(
            "Excluir tarefa",
            $"Tem certeza de que deseja excluir \"{Tarefa.Titulo}\"? Esta ação não pode ser desfeita.",
            "Excluir",
            "Cancelar");

        if (confirmar)
        {
            _tarefaService.Remover(Tarefa);
            await Shell.Current.GoToAsync(".."); // Volta para a lista (navegação hierárquica)
        }
    }
}
