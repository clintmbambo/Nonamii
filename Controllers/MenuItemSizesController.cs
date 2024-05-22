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
    public class MenuItemSizesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuItemExtrasRepo _menuItemExtrasRepo;

        public MenuItemSizesController(ApplicationDbContext context, IMenuItemExtrasRepo menuItemExtrasRepo)
        {
            _context = context;
            _menuItemExtrasRepo = menuItemExtrasRepo;
        }

        // GET: MenuItemSizes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MenuItemSizes
                .Include(m => m.MenuItem)
                .Include(m => m.Size)
                .Where(m => m.UserId == _menuItemExtrasRepo.GetUserId());

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MenuItemSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemSize = await _context.MenuItemSizes
                .Include(m => m.MenuItem)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemSize == null)
            {
                return NotFound();
            }

            return View(menuItemSize);
        }

        // GET: MenuItemSizes/Create
        public IActionResult Create()
        {
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name");
            return View();
        }

        // POST: MenuItemSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SizeId,MenuItemId,Price")] MenuItemSize menuItemSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItemSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Name", menuItemSize.MenuItemId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Name", menuItemSize.SizeId);
            return View(menuItemSize);
        }

        // GET: MenuItemSizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemSize = await _context.MenuItemSizes.FindAsync(id);
            if (menuItemSize == null)
            {
                return NotFound();
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemSize.MenuItemId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", menuItemSize.SizeId);
            return View(menuItemSize);
        }

        // POST: MenuItemSizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SizeId,MenuItemId,Price")] MenuItemSize menuItemSize)
        {
            if (id != menuItemSize.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItemSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemSizeExists(menuItemSize.Id))
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
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "Id", menuItemSize.MenuItemId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Id", menuItemSize.SizeId);
            return View(menuItemSize);
        }

        // GET: MenuItemSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemSize = await _context.MenuItemSizes
                .Include(m => m.MenuItem)
                .Include(m => m.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuItemSize == null)
            {
                return NotFound();
            }

            return View(menuItemSize);
        }

        // POST: MenuItemSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemSize = await _context.MenuItemSizes.FindAsync(id);
            if (menuItemSize != null)
            {
                _context.MenuItemSizes.Remove(menuItemSize);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemSizeExists(int id)
        {
            return _context.MenuItemSizes.Any(e => e.Id == id);
        }
    }
}
