using BlContainers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlContainers.Controllers;

public class RelatorioController : Controller
{
    private readonly AppDbContext _db;
    public RelatorioController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() =>
        View(await _db.BLs.Include(b => b.Containers).OrderBy(b => b.Numero).ToListAsync());
}
