using System.Globalization;
using TarefasApp.Models;

namespace TarefasApp.Converters;

/// <summary>
/// Converte um valor de Prioridade em uma cor para destacar visualmente o item na lista/detalhe.
/// </summary>
public class PrioridadeParaCorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Prioridade prioridade)
        {
            return prioridade switch
            {
                Prioridade.Alta => Color.FromArgb("#E53935"),
                Prioridade.Media => Color.FromArgb("#FB8C00"),
                Prioridade.Baixa => Color.FromArgb("#43A047"),
                _ => Colors.Gray
            };
        }
        return Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
