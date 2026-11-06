// Autora: Bianca Fonseca Dantas Ribeiro - CB3025683
using Microsoft.Extensions.Logging;
using TrackingApp.Services;
using TrackingApp.ViewModels;
using TrackingApp.Views;

namespace TrackingApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Serviço de dados (fictício) e ViewModel compartilhado entre as páginas
        builder.Services.AddSingleton<ITrackingService, FakeTrackingService>();
        builder.Services.AddSingleton<TrackingViewModel>();

        // Páginas (View)
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<ResultPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
