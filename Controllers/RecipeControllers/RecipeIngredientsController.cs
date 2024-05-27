using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nonamii.Controllers.RecipeControllers
{
    public class RecipeIngredientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRecipeIngredientRepo _recipeIngredientRepo;
        public RecipeIngredientsController(ApplicationDbContext context, IRecipeIngredientRepo recipeIngredientRepo)
        {
            _context = context;
            _recipeIngredientRepo = recipeIngredientRepo;
        }

        // GET: RecipeIngredients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Measurement)
                .Include(r => r.Recipe)
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId());

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RecipeIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Measurement)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredients
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId()), "Id", "Name");

            ViewData["MeasurementId"] = new SelectList(_context.Measurement
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId()), "Id", "Abbreviation");

            ViewData["RecipeId"] = new SelectList(_context.Recipes
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId()), "Id", "Title");

            return View();
        }

        // POST: RecipeIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,RecipeId,IngredientId,MeasurementId,amntValue")] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid)
            {
                recipeIngredient.UserId = _recipeIngredientRepo.GetUserId();
                _context.Add(recipeIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["IngredientId"] = new SelectList(_context.Ingredients
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId()), "Id", "Name");

            ViewData["MeasurementId"] = new SelectList(_context.Measurement
                .Where(m => m.UserId == _recipeIngredientRepo.GetUserId()), "Id", "Abbreviation");

            ViewData["RecipeId"] = new SelectList(_context.Recipes
                .Where(m => m.UserId == _recipeIngredientRepo .GetUserId()), "Id", "Title");

            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", recipeIngredient.IngredientId);
            ViewData["MeasurementId"] = new SelectList(_context.Measurement, "Id", "Id", recipeIngredient.MeasurementId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,RecipeId,IngredientId,MeasurementId,amntValue")] RecipeIngredient recipeIngredient)
        {
            if (id != recipeIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeIngredientExists(recipeIngredient.Id))
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
            ViewData["IngredientId"] = new SelectList(_context.Ingredients, "Id", "Id", recipeIngredient.IngredientId);
            ViewData["MeasurementId"] = new SelectList(_context.Measurement, "Id", "Id", recipeIngredient.MeasurementId);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "Id", "Id", recipeIngredient.RecipeId);
            return View(recipeIngredient);
        }

        // GET: RecipeIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeIngredient = await _context.RecipeIngredients
                .Include(r => r.Ingredient)
                .Include(r => r.Measurement)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredient == null)
            {
                return NotFound();
            }

            return View(recipeIngredient);
        }

        // POST: RecipeIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient != null)
            {
                _context.RecipeIngredients.Remove(recipeIngredient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeIngredientExists(int id)
        {
            return _context.RecipeIngredients.Any(e => e.Id == id);
        }
    }
}
