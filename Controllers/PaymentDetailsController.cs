using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nonamii.Data;
using Nonamii.Models;

namespace Nonamii.Controllers
{
    public class PaymentDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserOrdersRepo _userOrdersRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentDetailsController(ApplicationDbContext context, IUserOrdersRepo userOrdersRepo)
        {
            _context = context;
            _userOrdersRepo = userOrdersRepo;
        }

        // GET: PaymentDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentDetails.ToListAsync());
        }

        // GET: PaymentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDetails = await _context.PaymentDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            return View(paymentDetails);
        }

        // GET: PaymentDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Email,CardNumber,CVV,CardHolderName,Region")] PaymentDetails paymentDetails)
        {
            if (ModelState.IsValid)
            {
                paymentDetails.UserId = _userOrdersRepo.GetUserId();
                _context.Add(paymentDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentDetails);
        }

        // GET: PaymentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }
            return View(paymentDetails);
        }

        // POST: PaymentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Email,CardNumber,CVV,CardHolderName,Region")] PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDetailsExists(paymentDetails.Id))
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
            return View(paymentDetails);
        }

        // GET: PaymentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentDetails = await _context.PaymentDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            return View(paymentDetails);
        }

        // POST: PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetails != null)
            {
                _context.PaymentDetails.Remove(paymentDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDetailsExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.Id == id);
        }
    }
}
