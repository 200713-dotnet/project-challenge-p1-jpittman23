using System;
using System.Threading.Tasks;
using PizzaStore.Client.Models;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly PizzaStoreDBContext _DBContext;
        private readonly CartViewModel _cart;

        public OrderRepo(PizzaStoreDBContext DBcontext, CartViewModel Cart)
        {
            _DBContext = DBcontext;
            _cart = Cart;
        }
        public async Task CreateOrderAsync(OrderModel order)
        {
            order.PlaceTime = DateTime.Now;
            decimal OTotal = 0M;

            var product = _cart.cart;

            foreach (var item in product)
            {
                var odetail = new ODetail()
                {
                    Total = item.Total,
                    PizzaId = item.Pizzas.Id,
                    Cost = item.Pizzas.Price,
                    Order = order,
                };

                OTotal += odetail.Cost * odetail.Total;
                _DBContext.Odetail.Add(odetail);
            };

            await _DBContext.SaveChangesAsync();
        }
    }
}