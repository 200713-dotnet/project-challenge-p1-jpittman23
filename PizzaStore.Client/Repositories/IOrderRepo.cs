using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Repositories
{
    public interface IOrderRepo
    {
        Task CreateOrderAsync(OrderModel order);

    }
}