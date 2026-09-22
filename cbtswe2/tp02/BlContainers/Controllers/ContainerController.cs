using BlContainers.Data;
using BlContainers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlContainers.Controllers;

public class ContainerController : Controller
{
    private readonly AppDbContext _db;
    public ContainerController(AppDbContext db) => _db = db;

    private void CarregarListas(Container c = null)
    {
        ViewBag.BLs = new SelectList(_db.BLs.OrderBy(b => b.Numero), "ID", "Numero", c?.BLId);
        ViewBag.Tipos = new SelectList(new[] { "Dry", "Reefer" }, c?.Tipo);
        ViewBag.Tamanhos = new SelectList(new[] { 20, 40 }, c?.Tamanho);
    }

    public async Task<IActionResult> Index() =>
        View(await _db.Containers.Include(c => c.BL).OrderBy(c => c.Numero).ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var c = await _db.Containers.Include(x => x.BL).FirstOrDefaultAsync(x => x.ID == id);
        return c == null ? NotFound() : View(c);
    }

    public IActionResult Create(int? blId)
    {
        var c = new Container { BLId = blId };
        CarregarListas(c);
        return View(c);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Numero,Tipo,Tamanho,BLId")] Container c)
    {
        ModelState.Remove("BL");
        if (c.BLId != null && !await _db.BLs.AnyAsync(b => b.ID == c.BLId))
            ModelState.AddModelError("BLId", "BL inválido.");
        if (!ModelState.IsValid) { CarregarListas(c); return View(c); }
        _db.Add(c);
        await _db.SaveChangesAsync();
        TempData["Msg"] = "Container cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var c = await _db.Containers.FindAsync(id);
        if (c == null) return NotFound();
        CarregarListas(c);
        return View(c);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Numero,Tipo,Tamanho,BLId")] Container c)
    {
        if (id != c.ID) return NotFound();
        ModelState.Remove("BL");
        if (c.BLId != null && !await _db.BLs.AnyAsync(b => b.ID == c.BLId))
            ModelState.AddModelError("BLId", "BL inválido.");
        if (!ModelState.IsValid) { CarregarListas(c); return View(c); }
        _db.Update(c);
        await _db.SaveChangesAsync();
        TempData["Msg"] = "Container atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var c = await _db.Containers.Include(x => x.BL).FirstOrDefaultAsync(x => x.ID == id);
        return c == null ? NotFound() : View(c);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var c = await _db.Containers.FindAsync(id);
        if (c != null) { _db.Containers.Remove(c); await _db.SaveChangesAsync(); }
        TempData["Msg"] = "Container excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
