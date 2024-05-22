using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nonamii.Data;
using Nonamii.Models.Inventory;

namespace Nonamii.Controllers.MenuControllers
{
    public class MenuCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MenuCategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuCategory.Include(m => m.Category).Include(m => m.Menu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory
                .Include(m => m.Category)
                .Include(m => m.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // GET: MenuCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id");
            return View();
        }

        // POST: MenuCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,CategoryId")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", menuCategory.CategoryId);
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory.FindAsync(id);
            if (menuCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", menuCategory.CategoryId);
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuCategory.MenuId);
            return View(menuCategory);
        }

        // POST: MenuCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuId,CategoryId")] MenuCategory menuCategory)
        {
            if (id != menuCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuCategoryExists(menuCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", menuCategory.CategoryId);
            ViewData["MenuId"] = new SelectList(_context.Menu, "Id", "Id", menuCategory.MenuId);
            return View(menuCategory);
        }

        // GET: MenuCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuCategory = await _context.MenuCategory
                .Include(m => m.Category)
                .Include(m => m.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuCategory == null)
            {
                return NotFound();
            }

            return View(menuCategory);
        }

        // POST: MenuCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuCategory = await _context.MenuCategory.FindAsync(id);
            if (menuCategory != null)
            {
                _context.MenuCategory.Remove(menuCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuCategoryExists(int id)
        {
            return _context.MenuCategory.Any(e => e.Id == id);
        }
    }
}
