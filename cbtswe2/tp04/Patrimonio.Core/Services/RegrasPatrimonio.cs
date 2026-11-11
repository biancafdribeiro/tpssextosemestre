namespace Patrimonio.Core;

/// <summary>Gera o número de tombamento no formato CAT-AAAA-NNNNN.</summary>
public static class TombamentoService
{
    public static string Gerar(string categoria, int sequencia, int ano)
    {
        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("Categoria é obrigatória.", nameof(categoria));
        if (sequencia <= 0)
            throw new ArgumentOutOfRangeException(nameof(sequencia), "A sequência deve ser maior que zero.");

        var prefixo = new string(categoria.Trim().ToUpperInvariant().Take(3).ToArray());
        return $"{prefixo}-{ano}-{sequencia:D5}";
    }
}

/// <summary>Cálculo de depreciação linear (valor residual).</summary>
public static class DepreciacaoService
{
    public static decimal ValorAtual(decimal valorAquisicao, DateOnly dataAquisicao, int vidaUtilAnos, DateOnly hoje)
    {
        if (vidaUtilAnos <= 0)
            throw new ArgumentOutOfRangeException(nameof(vidaUtilAnos), "A vida útil deve ser positiva.");

        var anosDecorridos = (hoje.DayNumber - dataAquisicao.DayNumber) / 365.25;

        if (anosDecorridos <= 0) return valorAquisicao;
        if (anosDecorridos >= vidaUtilAnos) return 0m;

        var depreciacaoAnual = valorAquisicao / vidaUtilAnos;
        var valor = valorAquisicao - depreciacaoAnual * (decimal)anosDecorridos;
        return Math.Round(valor, 2);
    }
}
