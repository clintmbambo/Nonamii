using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nonamii.Data;

namespace Nonamii.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserOrdersRepo _userOrdersRepo;

        public OrdersController(ApplicationDbContext context, IUserOrdersRepo userOrdersRepo)
        {
            _context = context;
            _userOrdersRepo = userOrdersRepo;
        }

        // GET: Orders
        [Authorize(Roles = "Restaurant")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _context.Orders
                .Include(o => o.OrderStatus)
                .Where(m => m.UserId == _userOrdersRepo.GetUserId())
                .ToListAsync();

            return View(applicationDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Status");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,OrderStatusId,DateCreated,IsActive")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Status", order.OrderStatusId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Status", order.OrderStatusId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,OrderStatusId,DateCreated,IsActive")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.UserId = _userOrdersRepo.GetUserId();
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "Id", "Id", order.OrderStatusId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        //Handling Pending Orders.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Pending()
        {
            var pendingOrders = await _userOrdersRepo.GetPendingOrders();
            return View(pendingOrders);
        }
        public async Task<IActionResult> GetPendingUserOrders()
        {
            var orders = await _userOrdersRepo.GetPendingOrders();
            return View(orders);
        }
        public async Task<IActionResult> Confirm(int id)
        {
            var order = _context.Orders.Find(id);
            return View(order);
        }
        public async Task<IActionResult> CancelOrder(int id)
        {
            if(id > 0)
            {
                _userOrdersRepo.CancelOrder(id);
                return RedirectToAction("Success");
            }

            return RedirectToAction("GetPendingUserOrders");
        }
        public async Task<IActionResult> Success()
        {
            return View();
        }

        //Handling ALL Orders.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUserOrders()
        {
            var orders = await _userOrdersRepo.GetOrders();
            return View(orders);
        }

        public async Task<IActionResult> GetAllOrdersByUser()
        {
            var userOrders = await _userOrdersRepo.GetAllOrdersByUser();
            return View(userOrders);
        }

        //Handling Orders in Progress.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> InProgress()
        {
            var ordersInProgress = await _userOrdersRepo.GetOrdersInProgress();
            return View(ordersInProgress);
        }
        public async Task<IActionResult> GetUserOrdersInProgress()
        {
            return View();
        }

        //Handling Orders that are ready.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Ready()
        {
            var ordersReady = await _userOrdersRepo.GetOrdersReady();
            return View(ordersReady);
        }

        //Handling Orders that are cancelled.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Cancelled()
        {
            var cancelledOrders = await _userOrdersRepo.GetCancelledOrders();
            return View(cancelledOrders);
        }
    }
}
