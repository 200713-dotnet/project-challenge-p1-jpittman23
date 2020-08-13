
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PizzaStore.Storing;
using PizzaStore.Client.Repositories;
using PizzaStore.Client.Models;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PizzaController : Controller
    {
        private readonly PizzaStoreDBContext _DBContext;
        private readonly PizzaRepo _pizzaRepo;

        public PizzaController(PizzaStoreDBContext DBContext, PizzaRepo pizzaRepo)
        {
            _DBContext = DBContext;
            _pizzaRepo = pizzaRepo;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRepo.GetAllIncludedAsync());
        }

        // GET: Pizzas
        [AllowAnonymous]
        public async Task<IActionResult> AllPizzas()
        {
            var model = new PizzaViewModel()
            {
                PizzaList = await _pizzaRepo.GetAllIncludedAsync()
            };
            return View(model);
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // GET: Pizzas/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> DisplayDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            var PizzaToppings = await _DBContext.PizzaToppings.Where(x => x.PizzaId == id).Select(x => x.Topping.Name).ToListAsync();
            ViewBag.PizzaToppings = PizzaToppings;

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // GET: Pizzas
        [AllowAnonymous]

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl")] PizzaModel pizzas)
        {
            if (ModelState.IsValid)
            {
                _pizzaRepo.Add(pizzas);
                await _pizzaRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pizzas);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }
            return View(pizzas);
        }

        // POST: Pizzas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl")] PizzaModel pizzas)
        {
            if (id != pizzas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _pizzaRepo.Update(pizzas);
                    await _pizzaRepo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzasExists(pizzas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(pizzas);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pizzas = await _pizzaRepo.GetByIdAsync(id);
            _pizzaRepo.Remove(pizzas);
            await _pizzaRepo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PizzasExists(int id)
        {
            return _pizzaRepo.Exists(id);
        }
    }
}