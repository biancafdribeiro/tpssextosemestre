using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Patrimonio.Api.Dtos;
using Xunit;

namespace Patrimonio.Tests;

/// <summary>Usa um banco SQLite separado para cada execução de teste.</summary>
public class ApiFactory : WebApplicationFactory<Program>
{
    private readonly string _arquivo = $"test_{Guid.NewGuid():N}.db";

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        => builder.UseSetting("ConnectionStrings:Default", $"Data Source={_arquivo}");

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        try { if (File.Exists(_arquivo)) File.Delete(_arquivo); } catch { /* arquivo ainda em uso */ }
    }
}

public class BensApiTests(ApiFactory factory) : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CrudCompleto_CriarBuscarAtualizarExcluir()
    {
        // CREATE
        var novo = new BemRequest("Notebook Dell", "Informática", "Lab 1",
            new DateOnly(2024, 3, 10), 4500m, 5, "Ativo");

        var post = await _client.PostAsJsonAsync("/api/bens", novo);
        Assert.Equal(HttpStatusCode.Created, post.StatusCode);

        var criado = await post.Content.ReadFromJsonAsync<BemResponse>();
        Assert.NotNull(criado);
        Assert.StartsWith("INF-2024-", criado!.Tombamento);

        // READ
        var get = await _client.GetFromJsonAsync<BemResponse>($"/api/bens/{criado.Id}");
        Assert.Equal("Notebook Dell", get!.Descricao);

        // UPDATE
        var editado = novo with { Descricao = "Notebook Dell Latitude", Local = "Lab 2" };
        var put = await _client.PutAsJsonAsync($"/api/bens/{criado.Id}", editado);
        Assert.Equal(HttpStatusCode.OK, put.StatusCode);
        var atualizado = await put.Content.ReadFromJsonAsync<BemResponse>();
        Assert.Equal("Notebook Dell Latitude", atualizado!.Descricao);
        Assert.Equal(criado.Tombamento, atualizado.Tombamento); // tombamento não muda

        // DELETE
        var del = await _client.DeleteAsync($"/api/bens/{criado.Id}");
        Assert.Equal(HttpStatusCode.NoContent, del.StatusCode);

        var depois = await _client.GetAsync($"/api/bens/{criado.Id}");
        Assert.Equal(HttpStatusCode.NotFound, depois.StatusCode);
    }

    [Fact]
    public async Task Criar_ValorInvalido_RetornaBadRequest()
    {
        var invalido = new BemRequest("Cadeira", "Mobiliário", "", new DateOnly(2024, 1, 1), -10m, 10, "Ativo");
        var resp = await _client.PostAsJsonAsync("/api/bens", invalido);
        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
    }

    [Fact]
    public async Task Obter_IdInexistente_RetornaNotFound()
    {
        var resp = await _client.GetAsync("/api/bens/99999");
        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }
}
