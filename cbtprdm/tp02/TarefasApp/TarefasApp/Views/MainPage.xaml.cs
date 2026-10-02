using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Views;

public partial class MainPage : ContentPage
{
    private readonly TarefaService _tarefaService;

    public MainPage(TarefaService tarefaService)
    {
        InitializeComponent();
        _tarefaService = tarefaService;

        // BindingContext é o próprio serviço, pois ele expõe a ObservableCollection "Tarefas"
        // que o CollectionView consome e atualiza automaticamente.
        BindingContext = _tarefaService;
    }

    /// <summary>
    /// Navegação hierárquica: ao tocar em um item, empilha a página de detalhes
    /// passando a tarefa selecionada como parâmetro de rota.
    /// </summary>
    private async void OnTarefaTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is TarefaItem tarefa)
        {
            var parametros = new Dictionary<string, object>
            {
                { "Tarefa", tarefa }
            };

            await Shell.Current.GoToAsync(nameof(TarefaDetailPage), parametros);
        }
    }

    /// <summary>
    /// Abre o modal de adição de nova tarefa.
    /// </summary>
    private async void OnAdicionarClicked(object sender, EventArgs e)
    {
        var paginaAdicionar = new AdicionarTarefaPage(_tarefaService);
        await Navigation.PushModalAsync(new NavigationPage(paginaAdicionar));
    }
}
