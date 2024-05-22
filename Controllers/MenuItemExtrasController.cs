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
    [Authorize(Roles = "Restaurant")]
    public class MenuItemExtrasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuItemExtrasRepo _menuItemExtrasRepo;

        public MenuItemExtrasController(ApplicationDbContext context, IMenuItemExtrasRepo menuItemExtrasRepo)
        {
            _context = context;
            _menuItemExtrasRepo = menuItemExtrasRepo;
        }

        // GET: MenuItemExtras
        public async Task<IActionResult> Index()
        {
            return View(await _menuItemExtrasRepo.GetMenuItemExtrasAsync());
        }

        // GET: MenuItemExtras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemExtra = await _context.MenuItemExtras
                .Include(m => m.Extra)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemExtra == null)
            {
                return NotFound();
            }

            return View(menuItemExtra);
        }

        // GET: MenuItemExtras/Create
        public IActionResult Create()
        {
            ViewData["ExtraId"] = new SelectList(_context.Extras, "Id", "Name");
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name");
            return View();
        }

        // POST: MenuItemExtras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExtraId,MenuItemId,IsActive")] MenuItemExtra menuItemExtra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItemExtra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExtraId"] = new SelectList(_context.Extras, "Id", "Name", menuItemExtra.ExtraId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItemExtra.MenuItemId);
            return View(menuItemExtra);
        }

        // GET: MenuItemExtras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemExtra = await _context.MenuItemExtras.FindAsync(id);
            if (menuItemExtra == null)
            {
                return NotFound();
            }
            ViewData["ExtraId"] = new SelectList(_context.Extras, "Id", "Id", menuItemExtra.ExtraId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemExtra.MenuItemId);
            return View(menuItemExtra);
        }

        // POST: MenuItemExtras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExtraId,MenuItemId,IsActive")] MenuItemExtra menuItemExtra)
        {
            if (id != menuItemExtra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItemExtra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExtraExists(menuItemExtra.Id))
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
            ViewData["ExtraId"] = new SelectList(_context.Extras, "Id", "Id", menuItemExtra.ExtraId);
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemExtra.MenuItemId);
            return View(menuItemExtra);
        }

        // GET: MenuItemExtras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemExtra = await _context.MenuItemExtras
                .Include(m => m.Extra)
                .Include(m => m.MenuItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemExtra == null)
            {
                return NotFound();
            }

            return View(menuItemExtra);
        }

        // POST: MenuItemExtras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemExtra = await _context.MenuItemExtras.FindAsync(id);
            if (menuItemExtra != null)
            {
                _context.MenuItemExtras.Remove(menuItemExtra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExtraExists(int id)
        {
            return _context.MenuItemExtras.Any(e => e.Id == id);
        }
    }
}
