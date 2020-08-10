using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PizzaStore.Storing;
using PizzaStore.Client.Repositories;

namespace PizzaStore.Client.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StoreController : Controller
    {
        private readonly PizzaStoreDBContext _DBContext;
        private readonly IStoreRepo _StoreRepo;

        public StoreController(PizzaStoreDBContext DBContext, IStoreRepo StoreRepo)
        {
            _DBContext = DBContext;
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

        public IActionResult SeedDatabaseAsync()
        {
            _StoreRepo.ClearDatabase();
            _StoreRepo.SeedDB();
            return RedirectToAction("Index", "Pizzas", null);
        }

    }
}