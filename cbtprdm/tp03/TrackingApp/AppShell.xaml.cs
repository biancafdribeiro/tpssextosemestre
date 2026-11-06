// Autora: Bianca Fonseca Dantas Ribeiro - CB3025683
using TrackingApp.Views;

namespace TrackingApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rota da página de resultados (navegação feita pelo ViewModel)
        Routing.RegisterRoute("resultado", typeof(ResultPage));
    }
}
