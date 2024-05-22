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
    public class MenuTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuTypesRepo _menuTypesRepo;

        public MenuTypesController(ApplicationDbContext context, IMenuTypesRepo menuTypesRepo)
        {
            _context = context;
            _menuTypesRepo = menuTypesRepo;
        }

        // GET: MenuTypes
        public async Task<IActionResult> Index()
        {
            var menuTypes = await _menuTypesRepo.GetMenuTypesAsync();
            return View(menuTypes);
        }

        // GET: MenuTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuType = await _context.MenuType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuType == null)
            {
                return NotFound();
            }

            return View(menuType);
        }

        // GET: MenuTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] MenuType menuType)
        {
            if (ModelState.IsValid)
            {
                menuType.UserId = _menuTypesRepo.GetUserId();
                _context.Add(menuType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuType);
        }

        // GET: MenuTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuType = await _context.MenuType.FindAsync(id);
            if (menuType == null)
            {
                return NotFound();
            }
            return View(menuType);
        }

        // POST: MenuTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] MenuType menuType)
        {
            if (id != menuType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuTypeExists(menuType.Id))
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
            return View(menuType);
        }

        // GET: MenuTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuType = await _context.MenuType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menuType == null)
            {
                return NotFound();
            }

            return View(menuType);
        }

        // POST: MenuTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuType = await _context.MenuType.FindAsync(id);
            if (menuType != null)
            {
                _context.MenuType.Remove(menuType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuTypeExists(int id)
        {
            return _context.MenuType.Any(e => e.Id == id);
        }
    }
}
