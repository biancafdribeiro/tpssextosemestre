using BlContainers.Data;
using BlContainers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlContainers.Controllers;

public class BLController : Controller
{
    private readonly AppDbContext _db;
    public BLController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() =>
        View(await _db.BLs.Include(b => b.Containers).OrderBy(b => b.Numero).ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var bl = await _db.BLs.Include(b => b.Containers).FirstOrDefaultAsync(b => b.ID == id);
        return bl == null ? NotFound() : View(bl);
    }

    public IActionResult Create() => View(new BL());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Numero,Consignee,Navio")] BL bl)
    {
        if (!ModelState.IsValid) return View(bl);
        _db.Add(bl);
        await _db.SaveChangesAsync();
        TempData["Msg"] = "BL cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var bl = await _db.BLs.FindAsync(id);
        return bl == null ? NotFound() : View(bl);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,Numero,Consignee,Navio")] BL bl)
    {
        if (id != bl.ID) return NotFound();
        if (!ModelState.IsValid) return View(bl);
        _db.Update(bl);
        await _db.SaveChangesAsync();
        TempData["Msg"] = "BL atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var bl = await _db.BLs.Include(b => b.Containers).FirstOrDefaultAsync(b => b.ID == id);
        return bl == null ? NotFound() : View(bl);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bl = await _db.BLs.Include(b => b.Containers).FirstOrDefaultAsync(b => b.ID == id);
        if (bl == null) return NotFound();
        if (bl.Containers.Any())
        {
            TempData["Erro"] = "Não é possível excluir um BL que possui containers. Exclua os containers primeiro.";
            return RedirectToAction(nameof(Index));
        }
        _db.BLs.Remove(bl);
        await _db.SaveChangesAsync();
        TempData["Msg"] = "BL excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }
}
