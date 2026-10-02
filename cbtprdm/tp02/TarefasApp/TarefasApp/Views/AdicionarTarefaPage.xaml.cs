using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Views;

public partial class AdicionarTarefaPage : ContentPage
{
    private readonly TarefaService _tarefaService;

    public AdicionarTarefaPage(TarefaService tarefaService)
    {
        InitializeComponent();
        _tarefaService = tarefaService;
        PickerPrioridade.SelectedItem = "Media";
    }

    private async void OnAdicionarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryTitulo.Text))
        {
            await DisplayAlert("Atenção", "O título da tarefa é obrigatório.", "OK");
            return;
        }

        if (PickerPrioridade.SelectedItem is null)
        {
            await DisplayAlert("Atenção", "Selecione uma prioridade.", "OK");
            return;
        }

        var agora = DateTime.Now;
        var novaTarefa = new TarefaItem
        {
            Titulo = EntryTitulo.Text.Trim(),
            Descricao = EditorDescricao.Text?.Trim() ?? string.Empty,
            DataCriacao = agora,
            DataAtualizacao = agora,
            Prioridade = Enum.Parse<Prioridade>((string)PickerPrioridade.SelectedItem)
        };

        _tarefaService.Adicionar(novaTarefa);
        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}