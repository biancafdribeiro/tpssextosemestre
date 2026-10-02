using TarefasApp.Views;

namespace TarefasApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rotas registradas para permitir navegação hierárquica (push) e modal (via GoToAsync).
        Routing.RegisterRoute(nameof(TarefaDetailPage), typeof(TarefaDetailPage));
        Routing.RegisterRoute(nameof(AdicionarTarefaPage), typeof(AdicionarTarefaPage));
        Routing.RegisterRoute(nameof(EditarTarefaPage), typeof(EditarTarefaPage));
    }
}
