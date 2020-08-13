using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PizzaStore.Storing;
using PizzaStore.Client.Repositories;
using PizzaStore.Client.Models;

namespace PizzaStore.Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoreController : Controller
    {
        private readonly PizzaStoreDBContext _DBContext;
        private readonly StoreRepo _StoreRepo;
        private readonly LocationsRepo _LocationRepo;
        private readonly StoreViewModel _store;

        public StoreController(StoreViewModel store, PizzaStoreDBContext DBContext, StoreRepo StoreRepo,LocationsRepo LocationRepo)
        {
            _DBContext = DBContext;
            _store = store;
            _LocationRepo = LocationRepo;
            _StoreRepo = StoreRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ClearDatabaseAsync()
        {
            _StoreRepo.ClearDatabase();
            return RedirectToAction("Index", "Pizzas", null);
        }

        public async Task<IActionResult> SelectStore()
        {
            var model = new StoreViewModel()
            {
                StoreList = await _LocationRepo.GetAllIncludedAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Location(int pizzaId)
        {
            var selectedStore = await _LocationRepo.GetByIdAsync(pizzaId);
            return RedirectToAction("Index");
        }

        public IActionResult SeedDatabaseAsync()
        {
            _StoreRepo.ClearDatabase();
            _StoreRepo.SeedDB();
            return RedirectToAction("Index", "Pizzas", null);
        }

    }
}