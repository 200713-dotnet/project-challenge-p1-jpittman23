using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Client.Models;
using PizzaStore.Client.Repositories;
using PizzaStore.Domain.Factories;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly PizzaStoreDBContext _context;
        private readonly OrderRepo _orderRepo;
        private readonly CartViewModel _cart;
        private readonly UserManager<IdentityUser> _userMnger;
        public OrderController(PizzaStoreDBContext dbContext, OrderRepo orderRepo, CartViewModel cart)
        {
            _context = dbContext;
            _orderRepo = orderRepo;
            _cart = cart;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel order)
        {
            var products = await _cart.GetCartItemAsync();
            _cart.cart = products;

            if (products.Count == 0)
            {
                ModelState.AddModelError("", "Add Pizza First");
            }
            if (ModelState.IsValid)
            {
                await _orderRepo.CreateOrderAsync(order);
                await _cart.ClearCartAsync();

                return RedirectToAction("Thanks");
            }
            return View(order);
        }

        public IActionResult Thanks()
        {
          ViewBag.ThanksMessage = $"Thanks for your order!";
          return View();
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userMnger.GetUserAsync(HttpContext.User);
            bool isAdmin = await _userMnger.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                var all = await _context.order.Include(o => o.orderdet).ToListAsync();
                return View(all);
            }
            else
            {
                var orders = await _context.order.Include(o => o.orderdet).ToListAsync();
                return View(orders);
            }
        }
        public async Task<IActionResult> details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var orders = await _context.order.Include(o => o.orderdet).SingleOrDefaultAsync(m => m.OrderId == id);
            var user = await _userMnger.GetUserAsync(HttpContext.User);
            var userRoles = await _userMnger.GetRolesAsync(user);
            bool isAdmin = userRoles.Any(r => r == "Admin");

            if (orders == null)
            {
                return NotFound();
            }

            if (isAdmin == false)
            {
                var userId = _userMnger.GetUserId(HttpContext.User);
                if (orders.UserId != userId)
                {
                    return BadRequest("Permission Denied");
                }
            }

            var orderDetailsList = _context.Odetail.Include(o => o.Pizza).Include(o => o.Order)
                .Where(x => x.OId == orders.OrderId);

            ViewBag.OrderDetailsList = orderDetailsList;

            return View(orders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.order.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}