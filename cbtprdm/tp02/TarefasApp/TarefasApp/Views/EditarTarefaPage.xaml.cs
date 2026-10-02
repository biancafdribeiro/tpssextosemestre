using TarefasApp.Models;
using TarefasApp.Services;

namespace TarefasApp.Views;

public partial class EditarTarefaPage : ContentPage
{
    private readonly TarefaService _tarefaService;
    private readonly TarefaItem _tarefaOriginal;

    public EditarTarefaPage(TarefaService tarefaService, TarefaItem tarefa)
    {
        InitializeComponent();
        _tarefaService = tarefaService;
        _tarefaOriginal = tarefa;

        EntryTitulo.Text = tarefa.Titulo;
        EditorDescricao.Text = tarefa.Descricao;
        PickerPrioridade.SelectedItem = tarefa.Prioridade.ToString();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
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

        var tarefaAtualizada = new TarefaItem
        {
            Id = _tarefaOriginal.Id,
            Titulo = EntryTitulo.Text.Trim(),
            Descricao = EditorDescricao.Text?.Trim() ?? string.Empty,
            DataCriacao = _tarefaOriginal.DataCriacao,
            DataAtualizacao = DateTime.Now,
            Prioridade = Enum.Parse<Prioridade>((string)PickerPrioridade.SelectedItem)
        };

        _tarefaService.Atualizar(tarefaAtualizada);
        await Navigation.PopModalAsync();
    }

    private async void OnCancelarClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}