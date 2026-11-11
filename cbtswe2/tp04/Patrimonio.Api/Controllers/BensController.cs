using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Api.Data;
using Patrimonio.Api.Dtos;
using Patrimonio.Api.Models;
using Patrimonio.Core;

namespace Patrimonio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BensController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BemResponse>>> Listar(
        [FromQuery] string? categoria, [FromQuery] string? status)
    {
        var query = db.Bens.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(b => b.Categoria == categoria);
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(b => b.Status == status);

        var bens = await query.OrderBy(b => b.Id).ToListAsync();
        return Ok(bens.Select(Map));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BemResponse>> Obter(int id)
    {
        var bem = await db.Bens.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        return bem is null ? NotFound() : Ok(Map(bem));
    }

    [HttpPost]
    public async Task<ActionResult<BemResponse>> Criar(BemRequest req)
    {
        var ano = req.DataAquisicao.Year;
        var sequencia = await db.Bens.CountAsync(b => b.Categoria == req.Categoria) + 1;

        string tombamento;
        do
        {
            tombamento = TombamentoService.Gerar(req.Categoria, sequencia++, ano);
        } while (await db.Bens.AnyAsync(b => b.Tombamento == tombamento));

        var bem = new Bem
        {
            Tombamento = tombamento,
            Descricao = req.Descricao,
            Categoria = req.Categoria,
            Local = req.Local ?? "",
            DataAquisicao = req.DataAquisicao,
            ValorAquisicao = req.ValorAquisicao,
            VidaUtilAnos = req.VidaUtilAnos,
            Status = req.Status
        };

        db.Bens.Add(bem);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(Obter), new { id = bem.Id }, Map(bem));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BemResponse>> Atualizar(int id, BemRequest req)
    {
        var bem = await db.Bens.FirstOrDefaultAsync(b => b.Id == id);
        if (bem is null) return NotFound();

        // O tombamento não muda após o cadastro.
        bem.Descricao = req.Descricao;
        bem.Categoria = req.Categoria;
        bem.Local = req.Local ?? "";
        bem.DataAquisicao = req.DataAquisicao;
        bem.ValorAquisicao = req.ValorAquisicao;
        bem.VidaUtilAnos = req.VidaUtilAnos;
        bem.Status = req.Status;

        await db.SaveChangesAsync();
        return Ok(Map(bem));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var bem = await db.Bens.FirstOrDefaultAsync(b => b.Id == id);
        if (bem is null) return NotFound();

        db.Bens.Remove(bem);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static BemResponse Map(Bem b) => new(
        b.Id,
        b.Tombamento,
        b.Descricao,
        b.Categoria,
        b.Local,
        b.DataAquisicao,
        b.ValorAquisicao,
        b.VidaUtilAnos,
        b.Status,
        DepreciacaoService.ValorAtual(
            b.ValorAquisicao, b.DataAquisicao, b.VidaUtilAnos,
            DateOnly.FromDateTime(DateTime.Today)));
}
