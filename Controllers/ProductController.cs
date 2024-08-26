using Microsoft.AspNetCore.Mvc;
using FertilizerPetrokimia.Models;
using FertilizerPetrokimia.Data;
using Microsoft.EntityFrameworkCore;

namespace FertilizerPetrokimia.Controllers;

public class ProductController : Controller {
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationDbContext _context;

    public ProductController(ILogger<ProductController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (_context == null) {
            return Problem("database tidak ditemukan");
        }
        var listProduct = from m in _context.Products select m;
        return View(await listProduct.ToListAsync());
    }

    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("id, name, type, description, price")] Product product) 
    {
        if (ModelState.IsValid) 
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Detail(int? id) {
        if (id == null || _context.Products == null) {
            return NotFound();
        }
        var product = await _context.Products.FirstOrDefaultAsync(model => model.id == id);
        if (product == null) {
            return NotFound();
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Model == null) {
            return NotFound();
        }
        var productQ = await  _context.Products.FirstOrDefaultAsync(data => data.id == id);
        if (productQ == null) {
            return NotFound();
        }
        return View(productQ);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, [Bind("id, name, type, description, price")] Product product) {
        if (id != product.id) {
            return NotFound();
        }
        if (ModelState.IsValid) {
            try
            {
                   _context.Update(product);
                   await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await productExist(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context == null) {
            return NotFound();
        }
        var product = await _context.Products.FirstOrDefaultAsync(data => data.id == id);
        if (product == null) {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(int? id) {
        if (_context.Products == null) {
            return NotFound();
        }
        var product = await _context.Products.FindAsync(id);
        if (product != null) {
            _context.Products.Remove(product);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> productExist(int? id) {
        var product = await _context.Products.FirstOrDefaultAsync(model => model.id == id);
        return (product != null);
    }
}