using Patrimonio.Core;
using Xunit;

namespace Patrimonio.Tests;

public class TombamentoServiceTests
{
    [Fact]
    public void Gerar_DeveMontarFormatoCorreto()
    {
        var t = TombamentoService.Gerar("Informática", 1, 2024);
        Assert.Equal("INF-2024-00001", t);
    }

    [Fact]
    public void Gerar_SequenciaInvalida_LancaExcecao()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => TombamentoService.Gerar("Mobiliário", 0, 2024));
    }

    [Fact]
    public void Gerar_CategoriaVazia_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => TombamentoService.Gerar("  ", 1, 2024));
    }
}

public class DepreciacaoServiceTests
{
    [Fact]
    public void ValorAtual_NoDiaDaAquisicao_EhOValorOriginal()
    {
        var data = new DateOnly(2024, 1, 1);
        Assert.Equal(1000m, DepreciacaoService.ValorAtual(1000m, data, 10, data));
    }

    [Fact]
    public void ValorAtual_AposVidaUtil_EhZero()
    {
        var aquisicao = new DateOnly(2010, 1, 1);
        Assert.Equal(0m, DepreciacaoService.ValorAtual(1000m, aquisicao, 10, new DateOnly(2030, 1, 1)));
    }

    [Fact]
    public void ValorAtual_NoMeioDaVidaUtil_ReduzPelaMetade()
    {
        var aquisicao = new DateOnly(2020, 1, 1);
        var hoje = aquisicao.AddDays(1826); // ~5 anos
        var valor = DepreciacaoService.ValorAtual(1000m, aquisicao, 10, hoje);
        Assert.InRange(valor, 499m, 501m);
    }

    [Fact]
    public void ValorAtual_VidaUtilZero_LancaExcecao()
    {
        var d = new DateOnly(2020, 1, 1);
        Assert.Throws<ArgumentOutOfRangeException>(() => DepreciacaoService.ValorAtual(1000m, d, 0, d));
    }
}
