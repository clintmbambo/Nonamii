using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nonamii.Data;
using Nonamii.Models.Inventory;

namespace Nonamii.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuItemIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuItemIngredientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuItemIngredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItemIngredients
                .Include(m => m.Ingredient)
                .Include(m => m.MenuItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItemIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemIngredient = await _context.MenuItemIngredients
                .Include(m => m.Ingredient)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemIngredient == null)
            {
                return NotFound();
            }

            return View(menuItemIngredient);
        }

        // GET: MenuItemIngredients/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name");
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name");
            return View();
        }

        // POST: MenuItemIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuItemId,IngredientId")] MenuItemIngredient menuItemIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItemIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Name", menuItemIngredient.IngredientId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItemIngredient.MenuItemId);
            return View(menuItemIngredient);
        }

        // GET: MenuItemIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemIngredient = await _context.MenuItemIngredients.FindAsync(id);
            if (menuItemIngredient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", menuItemIngredient.IngredientId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemIngredient.MenuItemId);
            return View(menuItemIngredient);
        }

        // POST: MenuItemIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuItemId,IngredientId")] MenuItemIngredient menuItemIngredient)
        {
            if (id != menuItemIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItemIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemIngredientExists(menuItemIngredient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", menuItemIngredient.IngredientId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemIngredient.MenuItemId);
            return View(menuItemIngredient);
        }

        // GET: MenuItemIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemIngredient = await _context.MenuItemIngredients
                .Include(m => m.Ingredient)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemIngredient == null)
            {
                return NotFound();
            }

            return View(menuItemIngredient);
        }

        // POST: MenuItemIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemIngredient = await _context.MenuItemIngredients.FindAsync(id);
            if (menuItemIngredient != null)
            {
                _context.MenuItemIngredients.Remove(menuItemIngredient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemIngredientExists(int id)
        {
            return _context.MenuItemIngredients.Any(e => e.Id == id);
        }
    }
}
