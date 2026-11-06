// Autora: Bianca Fonseca Dantas Ribeiro - CB3025683
namespace TrackingApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
