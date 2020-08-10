using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Models
{
    public class CartViewModel
    {
        public string CartIdentity { get; set; }
        public List<CartModel> cart { get; set; }
        private readonly PizzaStoreDBContext _DBContext;
        public CartViewModel(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public static CartViewModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

            var context = services.GetService<PizzaStoreDBContext>();
            string cartId = session.GetString("CartId");

            session.SetString("CartId", cartId);

            return new CartViewModel(context) { CartIdentity = cartId };
        }
        public async Task AddToCartAsync(PizzaModel pizza, int amount)
        {
            var CartItem =
                    await _DBContext.cart.SingleOrDefaultAsync(
                        p => p.Pizzas.Id == pizza.Id && p.Cartitem == CartIdentity);

            if (CartItem == null)
            {
                CartItem = new CartModel
                {
                    Cartitem = CartIdentity,
                    Pizzas = pizza,
                    Total = 1
                };

                _DBContext.cart.Add(CartItem);
            }
            else
            {
                CartItem.Total++;
            }

            await _DBContext.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCartAsync(PizzaModel pizza)
        {
            var CartItem =
                    await _DBContext.cart.SingleOrDefaultAsync(
                        s => s.Pizzas.Id == pizza.Id && s.Cartitem == CartIdentity);

            var localAmount = 0;

            if (CartItem != null)
            {
                if (CartItem.Total > 1)
                {
                    CartItem.Total--;
                    localAmount = CartItem.Total;
                }
                else
                {
                    _DBContext.cart.Remove(CartItem);
                }
            }

            await _DBContext.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<CartModel>> GetCartItemAsync()
        {
            return cart ??
                   (cart = await
                       _DBContext.cart.Where(c => c.Cartitem == CartIdentity)
                           .Include(p => p.Pizzas)
                           .ToListAsync());
        }

        public async Task ClearCartAsync()
        {
            var cartItems = _DBContext
                .cart
                .Where(cart => cart.Cartitem == CartIdentity);

            _DBContext.cart.RemoveRange(cartItems);

            await _DBContext.SaveChangesAsync();
        }

        public decimal GetCartTotal()
        {
            var total = _DBContext.cart.Where(c => c.Cartitem == CartIdentity)
                .Select(c => c.Pizzas.Price * c.Total).Sum();
            return total;
        }


    }
}