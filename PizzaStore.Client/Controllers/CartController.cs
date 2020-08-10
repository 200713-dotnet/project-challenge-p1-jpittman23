using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaStore.Client.Models;
using PizzaStore.Client.Repositories;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Controllers
{
    public class CartController : Controller
    {
        private readonly PizzaRepo _pizzaRepo;
        private readonly PizzaStoreDBContext _DBContext;
        private readonly CartViewModel _Cart;

        public CartController(PizzaRepo pizzaRepo, CartViewModel Cart, PizzaStoreDBContext DBContext)
        {
            _pizzaRepo = pizzaRepo;
            _Cart = Cart;
            _DBContext = DBContext;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _Cart.GetCartItemAsync();
            _Cart.cart = items;

            var ItemModel = new ItemViewModel
            {
                Cart = _Cart,
                CartTotal = _Cart.GetCartTotal()
            };

            return View(ItemModel);
        }

        public async Task<IActionResult> AddToShoppingCart(int pizzaId)
        {
            var selectedPizza = await _pizzaRepo.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await _Cart.AddToCartAsync(selectedPizza, 1);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromShoppingCart(int pizzaId)
        {
            var selectedPizza = await _pizzaRepo.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await _Cart.RemoveFromCartAsync(selectedPizza);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCart()
        {
            await _Cart.ClearCartAsync();

            return RedirectToAction("Index");
        }
    }
}