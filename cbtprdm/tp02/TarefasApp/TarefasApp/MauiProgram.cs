using Microsoft.Extensions.Logging;
using TarefasApp.Services;
using TarefasApp.Views;

namespace TarefasApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        // Observação: este projeto não inclui arquivos .ttf customizados em Resources/Fonts,
        // por isso usamos as fontes padrão da plataforma (nenhuma chamada a ConfigureFonts
        // é necessária). Se desejar fontes customizadas, adicione os .ttf em Resources/Fonts
        // e registre-os aqui com builder.UseMauiApp<App>().ConfigureFonts(...).

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Serviço de tarefas compartilhado (singleton) entre as páginas.
        builder.Services.AddSingleton<TarefaService>();

        // Páginas registradas via DI para receber o TarefaService automaticamente.
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<TarefaDetailPage>();
        builder.Services.AddTransient<AdicionarTarefaPage>();
        builder.Services.AddTransient<EditarTarefaPage>();

        return builder.Build();
    }
}
